namespace Mx.Hyperway.Inbound.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    using log4net;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using MimeKit;
    using MimeKit.Cryptography;

    using Mx.Hyperway.As2.Code;
    using Mx.Hyperway.As2.Inbound;
    using Mx.Hyperway.As2.Lang;
    using Mx.Hyperway.As2.Util;
    using Mx.Tools;

    using zipkin4net;

    using Trace = zipkin4net.Trace;

    [Route("api/as2")]
    public class As2Controller
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(As2Controller));

        private readonly Func<As2InboundHandler> inboundHandlerProvider;

        private readonly SMimeMessageFactory sMimeMessageFactory;

        private readonly HttpContext httpContext;

        public As2Controller(
            IHttpContextAccessor contextAccessor,
            Func<As2InboundHandler> inboundHandlerProvider,
            SMimeMessageFactory sMimeMessageFactory)
        {
            this.httpContext = contextAccessor.HttpContext;
            this.inboundHandlerProvider = inboundHandlerProvider;
            this.sMimeMessageFactory = sMimeMessageFactory;
        }

        [HttpGet]
        public ContentResult DoGet()
        {
            return new ContentResult() { StatusCode = StatusCodes.Status200OK, Content = "Hello AS2 world\n" };
        }

        /// <summary>
        /// Receives the POST'ed AS2 message.
        /// Important to note that the HTTP headers contains the MIME headers for the payload.
        /// Since the the request can only be read once, using getReader()/getInputStream()
        /// </summary>
        [HttpPost]
        public void DoPost()
        {
            var headers = this.httpContext.Request.Headers;
            var messageId = headers["message-id"];
            if (string.IsNullOrWhiteSpace(messageId))
            {
                var errorResult = new ContentResult();
                errorResult.StatusCode = StatusCodes.Status400BadRequest;
                errorResult.Content = "Header field 'Message-ID' not found.";
                throw new NotSupportedException("error management");
                // return errorResult;
            }


            Trace root = Trace.Create();
            root.Record(Annotations.ServiceName("as2servlet.post"));
            root.Record(Annotations.ServerRecv());
            root.Record(Annotations.Tag("message-id", messageId));

            Logger.Debug("Receiving HTTP POST request");
            try
            {
                // Read MIME message
                var bodyStream = this.httpContext.Request.Body;
                var bodyData = bodyStream.ToBuffer();
                MimeMessage mimeMessage =
                    MimeMessageHelper.CreateMimeMessageAssistedByHeaders(bodyData.ToStream(), headers);

                try
                {
                    Trace span = root.Child();
                    span.Record(Annotations.ServiceName("as2message"));
                    span.Record(Annotations.ServerRecv());
                    MimeMessage mdn = this.inboundHandlerProvider().Receive(headers, mimeMessage);
                    span.Record(Annotations.ServerSend());

                    span = root.Child();
                    span.Record(Annotations.ServiceName("mdn"));
                    span.Record(Annotations.ServerRecv());

                    this.WriteMdn(this.httpContext.Response, mdn, (int)HttpStatusCode.OK);
                    span.Record(Annotations.ServerSend());

                }
                catch (HyperwayAs2InboundException e)
                {
                    String identifier = Guid.NewGuid().ToString();
                    Logger.ErrorFormat("Error [{0}] {1}", identifier, e);

                    // Open message for reading
                    SMimeReader sMimeReader = new SMimeReader(mimeMessage);

                    // Begin builder
                    MdnBuilder mdnBuilder = MdnBuilder.NewInstance(mimeMessage);
                    // Original Message-Id
                    mdnBuilder.AddHeader(MdnHeader.OriginalMessageId, headers[As2Header.MessageId]);
                    // Disposition from exception
                    mdnBuilder.AddHeader(MdnHeader.Disposition, e.Disposition);
                    mdnBuilder.AddText(String.Format("Error [{0}]", identifier), e.Message);

                    // Build and add headers
                    MimeMessage mdn = this.sMimeMessageFactory.CreateSignedMimeMessage(
                        mdnBuilder.Build(),
                        sMimeReader.GetDigestMethod());
                    mdn.Headers.Add(As2Header.As2Version, As2Header.Version);
                    mdn.Headers.Add(As2Header.As2From, headers[As2Header.As2To]);
                    mdn.Headers.Add(As2Header.As2To, headers[As2Header.As2From]);
                    this.WriteMdn(this.httpContext.Response, mdn, (int)HttpStatusCode.BadRequest);
                }
            }
            catch (Exception e)
            {
                root.Record(Annotations.Tag("exception", e.Message));

                // Unexpected internal error, cannot proceed, return HTTP 500 and partly MDN to indicating the problem
                Logger.ErrorFormat("Internal error occured: {0}", e.Message);
                Logger.Error("Attempting to return MDN with explanatory message and HTTP 500 status");

                // TODO: manage failure
                this.WriteFailureWithExplanation(this.httpContext.Request, this.httpContext.Response, e);
            }

            // MDC.clear();
            root.Record(Annotations.ServerSend());
        }

        /// <summary>
        /// If the AS2 message processing failed with an exception, we have an internal error and act accordingly 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="e"></param>
        private void WriteFailureWithExplanation(HttpRequest request, HttpResponse response, Exception e)
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            Logger.Error("Internal error: " + e.Message, e);

            Logger.Debug("Request headers");
            foreach (var header in request.Headers)
            {
                Logger.DebugFormat("=> {0}: {1}", header.Key, header.Value);
            }

            var message = Encoding.ASCII.GetBytes("INTERNAL ERROR!!");
            response.Body.Write(message, 0, message.Length);

            // Being helpful to those who must read the error logs
            Logger.Error("\n---------- REQUEST FAILURE INFORMATION ENDS HERE --------------");
        }

        protected void WriteMdn(HttpResponse response, MimeMessage mdn, int status)
        {
            response.StatusCode = status;

            foreach (var header in mdn.Headers)
            {
                response.Headers.Add(header.Field, header.Value);
            }

            var contentType = ((MultipartSigned)mdn.Body).Headers[HeaderId.ContentType];
            response.Headers.Add("Content-Type", this.NormalizeHeaderValue(contentType));

            // Write MDN to response without header names.
            // Force 7bit encoding in MIME Response
            MultipartSigned mSigned = mdn.Body as MultipartSigned;
            Debug.Assert(mSigned != null, nameof(mSigned) + " != null");
            MultipartReport mReport = mSigned[0] as MultipartReport;
            Debug.Assert(mReport != null, nameof(mReport) + " != null");
            TextPart textReport = mReport[0] as TextPart;
            Debug.Assert(textReport != null, nameof(textReport) + " != null");
            textReport.ContentTransferEncoding = ContentEncoding.SevenBit;
            var notReport = mReport[1] as MessageDispositionNotification;
            Debug.Assert(notReport != null, nameof(notReport) + " != null");
            notReport.ContentTransferEncoding = ContentEncoding.SevenBit;
            mdn.Headers.Add(HeaderId.Date, As2DateUtil.Rfc822.GetFormat(DateTime.Now));
            mdn.WriteTo(response.Body);
        }

        private string NormalizeHeaderValue(string value)
        {
            var result = Regex.Replace(value, @"[\t\r\n]", string.Empty);
            return result;
        }
    }
}