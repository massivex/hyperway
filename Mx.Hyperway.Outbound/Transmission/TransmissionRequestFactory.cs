﻿namespace Mx.Hyperway.Outbound.Transmission
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

        private readonly ContentDetector contentDetector;

        private readonly ContentWrapper contentWrapper;

        public TransmissionRequestFactory(ContentDetector contentDetector, ContentWrapper contentWrapper, Trace tracer)
            : base(tracer)
        {
            this.contentDetector = contentDetector;
            this.contentWrapper = contentWrapper;
        }

        public TransmissionMessage newInstance(Stream inputStream)
        {
            Trace trace = Trace.Create();
            trace.Record(Annotations.ServiceName(this.GetType().Name));
            trace.Record(Annotations.ClientSend());
            // Span root = tracer.newTrace().name(getClass().getSimpleName()).start();
            try
            {
                return this.perform(inputStream, trace);
            }
            finally
            {
                trace.Record(Annotations.ClientRecv());
                //root.finish();
            }
        }

        public TransmissionMessage newInstance(Stream inputStream, Trace root) 
        {
            var trace = root.Child();
            trace.Record(Annotations.ServiceName(this.GetType().Name));
            trace.Record(Annotations.ClientSend());
            // Span span = tracer.newChild(root.context()).name(getClass().getSimpleName()).start();
            try
            {
                return this.perform(inputStream, root);
            }
            finally
            {
                trace.Record(Annotations.ClientRecv());
            }
        }

        private TransmissionMessage perform(Stream inputStream, Trace root)
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
                        span.Record(Annotations.Tag("identifier", header.getIdentifier().getIdentifier()));
                    }
                } catch (SbdhException e) {
                    span.Record(Annotations.Tag("exception", e.Message));
                    throw e;
                } finally {
                    span.Record(Annotations.ClientRecv());
                }

                // Create transmission request.
                return new DefaultTransmissionMessage(header, peekingInputStream.newInputStream());
            }
            catch (SbdhException e)
            {
                byte[] payload = peekingInputStream.getContent();

                // Detect header from content.
                Trace span = root.Child();
                span.Record(Annotations.ServiceName("Detect SBDH from content"));
                span.Record(Annotations.ClientSend());
                try
                {
                    header = this.contentDetector.parse(payload.ToStream());
                    span.Record(Annotations.Tag("identifier", header.getIdentifier().getIdentifier()));
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
                    wrappedContent = this.contentWrapper.wrap(payload.ToStream(), header);
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