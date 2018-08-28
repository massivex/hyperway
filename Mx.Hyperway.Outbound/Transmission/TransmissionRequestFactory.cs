namespace Mx.Hyperway.Outbound.Transmission
{
    using System.IO;

    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Api.Transformer;
    using Mx.Hyperway.Commons.IO;
    using Mx.Hyperway.Commons.Tracing;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh;
    using Mx.Peppol.Sbdh.Lang;
    using Mx.Tools;

    using zipkin4net;

    public class TransmissionRequestFactory : Traceable
    {

        private readonly IContentDetector contentDetector;

        private readonly IContentWrapper contentWrapper;

        public TransmissionRequestFactory(IContentDetector contentDetector, IContentWrapper contentWrapper, Trace tracer)
            : base(tracer)
        {
            this.contentDetector = contentDetector;
            this.contentWrapper = contentWrapper;
        }

        public ITransmissionMessage NewInstance(Stream inputStream)
        {
            Trace trace = Trace.Create();
            trace.Record(Annotations.ServiceName(this.GetType().Name));
            trace.Record(Annotations.ClientSend());
            try
            {
                return this.Perform(inputStream, trace);
            }
            finally
            {
                trace.Record(Annotations.ClientRecv());
            }
        }

        public ITransmissionMessage NewInstance(Stream inputStream, Trace root) 
        {
            var trace = root.Child();
            trace.Record(Annotations.ServiceName(this.GetType().Name));
            trace.Record(Annotations.ClientSend());
            try
            {
                return this.Perform(inputStream, root);
            }
            finally
            {
                trace.Record(Annotations.ClientRecv());
            }
        }

        private ITransmissionMessage Perform(Stream inputStream, Trace root)
        {
            PeekingInputStream peekingInputStream = new PeekingInputStream(inputStream);

            // Read header from content to send.
            Header header;
            try
            {
                // Read header from SBDH.
                var span = root.Child();
                span.Record(Annotations.ServiceName("Reading SBDH"));
                span.Record(Annotations.ClientSend());
                try {
                    using (SbdReader sbdReader = SbdReader.newInstance(peekingInputStream))
                    {
                        header = sbdReader.getHeader();
                        span.Record(Annotations.Tag("identifier", header.Identifier.Identifier));
                    }
                } catch (SbdhException e) {
                    span.Record(Annotations.Tag("exception", e.Message));
                    throw;
                } finally {
                    span.Record(Annotations.ClientRecv());
                }

                // Create transmission request.
                return new DefaultTransmissionMessage(header, peekingInputStream.NewInputStream());
            }
            catch (SbdhException)
            {
                byte[] payload = peekingInputStream.GetContent();

                // Detect header from content.
                Trace span = root.Child();
                span.Record(Annotations.ServiceName("Detect SBDH from content"));
                span.Record(Annotations.ClientSend());
                try
                {
                    header = this.contentDetector.Parse(payload.ToStream());
                    span.Record(Annotations.Tag("identifier", header.Identifier.Identifier));
                }
                catch (HyperwayContentException ex)
                {
                    span.Record(Annotations.Tag("exception", ex.Message));
                    throw new HyperwayContentException(ex.Message, ex);
                }
                finally
                {
                    span.Record(Annotations.ClientRecv());
                }

                // Wrap content in SBDH.
                span = root.Child();
                span.Record(Annotations.ServiceName("Wrap content in SBDH"));
                span.Record(Annotations.ClientSend());
                Stream wrappedContent;
                try
                {
                    wrappedContent = this.contentWrapper.Wrap(payload.ToStream(), header);
                }
                catch (HyperwayContentException ex)
                {
                    span.Record(Annotations.Tag("exception", ex.Message));
                    throw;
                }
                finally
                {
                    span.Record(Annotations.ClientRecv());
                }

                // Create transmission request.
                return new DefaultTransmissionMessage(header, wrappedContent);
            }
        }
    }

}
