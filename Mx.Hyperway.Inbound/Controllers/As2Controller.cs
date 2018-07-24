namespace Mx.Hyperway.Inbound.Controllers
{
    using System;

    using log4net;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using MimeKit;

    using Mx.Hyperway.As2.Lang;
    using Mx.Hyperway.As2.Util;

    using zipkin4net;

    [Route("api/as2")]
    public class As2Controller
    {
        public static readonly ILog LOGGER = LogManager.GetLogger(typeof(As2Controller));

        // private readonly Func<As2InboundHandler> inboundHandlerProvider;

        private readonly SMimeMessageFactory sMimeMessageFactory;

        private readonly HttpContext httpContext;

        public As2Controller(
            IHttpContextAccessor contextAccessor,
            // Func<As2InboundHandler> inboundHandlerProvider,
            SMimeMessageFactory sMimeMessageFactory)
        {
            this.httpContext = contextAccessor.HttpContext;
            // this.inboundHandlerProvider = inboundHandlerProvider;
            this.sMimeMessageFactory = sMimeMessageFactory;
        }

        [HttpGet]
        public ContentResult doGet()
        {
            return new ContentResult() { StatusCode = StatusCodes.Status200OK, Content = "Hello AS2 world\n" };
        }

        /**
         * Receives the POST'ed AS2 message.
         * <p>
         * Important to note that the HTTP headers contains the MIME headers for the payload.
         * Since the the request can only be read once, using getReader()/getInputStream()
         */
        [HttpPost]
        protected ContentResult doPost()
        {
            var headers = this.httpContext.Request.Headers;
            var messageId = headers["message-id"];
            if (string.IsNullOrWhiteSpace(messageId))
            {
                var errorResult = new ContentResult();
                errorResult.StatusCode = StatusCodes.Status400BadRequest;
                errorResult.Content = "Header field 'Message-ID' not found.";
                return errorResult;
            }


            Trace root = Trace.Create();
            root.Record(Annotations.ServiceName("as2servlet.post"));
            root.Record(Annotations.ServerRecv());
            root.Record(Annotations.Tag("message-id", messageId));

            // MDC.Add("message-id", request.getHeader("message-id"));

            LOGGER.Debug("Receiving HTTP POST request");

            // Read all headers
            // InternetHeaders headers = copyHttpHeadersIntoMap(request);

            // Receives the data, validates the headers, signature etc., invokes the persistence handler
            // and finally returns the MdnData to be sent back to the caller
            try
            {
                // Read MIME message

                MimeMessage mimeMessage =
                    MimeMessageHelper.createMimeMessageAssistedByHeaders(this.httpContext.Request.Body, headers);

                try
                {
                    // Performs the actual reception of the message by parsing the HTTP POST request
                    // persisting the payload etc.

                    Trace span = root.Child();
                    span.Record(Annotations.ServiceName("as2message"));

                    span.Record(Annotations.ServerRecv());
                    // TODO: MDN creation
                    // MimeMessage mdn = inboundHandlerProvider.get().receive(headers, mimeMessage);
                    span.Record(Annotations.ServerSend());

                    // Returns the MDN
                    span = root.Child();
                    span.Record(Annotations.ServiceName("mdn"));
                    span.Record(Annotations.ServerRecv());
                    // TODO: writeMdn(response, mdn, HttpServletResponse.SC_OK);
                    span.Record(Annotations.ServerSend());

                }
                catch (HyperwayAs2InboundException e)
                {
                    String identifier = Guid.NewGuid().ToString();
                    LOGGER.ErrorFormat("Error [{0}] {1}", identifier, e);

                    // Open message for reading
                    SMimeReader sMimeReader = new SMimeReader(mimeMessage);

                    // Begin builder
                    // TODO: MdnBuilder
                    // MdnBuilder mdnBuilder = MdnBuilder.newInstance(mimeMessage);

                    //                // Original Message-Id
                    //                mdnBuilder.addHeader(MdnHeader.ORIGINAL_MESSAGE_ID, headers.getHeader(As2Header.MESSAGE_ID)[0]);

                    //                // Disposition from exception
                    //                mdnBuilder.addHeader(MdnHeader.DISPOSITION, e.getDisposition());
                    //                mdnBuilder.addText(String.format("Error [%s]", identifier), e.getMessage());

                    //                // Build and add headers
                    //                MimeMessage mdn = sMimeMessageFactory.createSignedMimeMessage(
                    //                        mdnBuilder.build(), sMimeReader.getDigestMethod());
                    //                  mdn.setHeader(As2Header.AS2_VERSION, As2Header.VERSION);
                    //                mdn.setHeader(As2Header.AS2_FROM, headers.getHeader(As2Header.AS2_TO)[0]);
                    //                mdn.setHeader(As2Header.AS2_TO, headers.getHeader(As2Header.AS2_FROM)[0]);

                    //                writeMdn(response, mdn, HttpServletResponse.SC_BAD_REQUEST);
                }
            }
            catch (Exception e)
            {
                root.Record(Annotations.Tag("exception", e.Message));

                // Unexpected internal error, cannot proceed, return HTTP 500 and partly MDN to indicating the problem
                LOGGER.ErrorFormat("Internal error occured: {0}", e.Message);
                LOGGER.Error("Attempting to return MDN with explanatory message and HTTP 500 status");

                // TODO: manage failure
                // writeFailureWithExplanation(request, response, e);
            }

            // MDC.clear();
            root.Record(Annotations.ServerSend());
            return null;
        }

        //protected void writeMdn(HttpServletResponse response, MimeMessage mdn, int status) //throws MessagingException, IOException
            //{
            //    // Set HTTP status.
            //    response.setStatus(status);

            //    // Add headers and collect header names.
            //    String[] headers = Collections.list((Enumeration<Header>)mdn.getAllHeaders()).stream()
            //        .peek(h->response.setHeader(h.getName(), h.getValue())).map(Header::getName).toArray(String[]::new);

            //    // Write MDN to response without header names.
            //    mdn.writeTo(response.getOutputStream(), headers);
            //}

            /**
             * Dumps the http request headers of the request
             */
            //private void logRequestHeaders(HttpServletRequest request)
            //{
            //    LOGGER.debug("Request headers:");
            //    Collections.list(request.getHeaderNames())
            //        .forEach(name->LOGGER.debug("=> {}: {}", name, request.getHeader(name)));
            //}

            /**
             * If the AS2 message processing failed with an exception, we have an internal error and act accordingly
             */
            //void writeFailureWithExplanation(HttpServletRequest request, HttpServletResponse response, Exception e)
            //    //           throws IOException
            //{
            //    response.setStatus(HttpServletResponse.SC_INTERNAL_SERVER_ERROR);

            //    LOGGER.error("Internal error: " + e.getMessage(), e);

            //    logRequestHeaders(request);

            //    response.getWriter().write("INTERNAL ERROR!!");
            //    // Being helpful to those who must read the error logs
            //    LOGGER.error("\n---------- REQUEST FAILURE INFORMATION ENDS HERE --------------");
            //}

            /**
             * Copies the http request headers into an InternetHeaders object, which is more usefull when working with MIME.
             */
            //private InternetHeaders copyHttpHeadersIntoMap(HttpServletRequest request)
            //{
            //    InternetHeaders internetHeaders = new InternetHeaders();
            //    Collections.list(request.getHeaderNames())
            //        .forEach(name->internetHeaders.addHeader(name, request.getHeader(name)));
            //    return internetHeaders;
            //}

            /**
             * Allows for simple http GET requests
             */
            //protected void doGet(HttpServletRequest request, HttpServletResponse response)
            //    //           throws ServletException, IOException
            //{
            //    response.setStatus(HttpServletResponse.SC_OK);
            //    response.getOutputStream().println("Hello AS2 world\n");
            //}
        }

}