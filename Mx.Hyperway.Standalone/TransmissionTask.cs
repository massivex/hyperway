namespace Mx.Hyperway.Standalone
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using log4net;

    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Commons.FileSystem;
    using Mx.Hyperway.Commons.Interop;
    using Mx.Hyperway.Outbound.Transmission;
    using Mx.Peppol.Common.Model;

    using zipkin4net;

    using Trace = zipkin4net.Trace;

    public class TransmissionTask : ICallable<TransmissionResult>
    {

        public static readonly ILog log = LogManager.GetLogger(typeof(TransmissionTask));

        private readonly TransmissionParameters parameters;

        private readonly FileInfo xmlPayloadFile;

        public TransmissionTask(TransmissionParameters parameters, FileInfo xmlPayloadFile)
        {
            this.parameters = parameters;
            this.xmlPayloadFile = xmlPayloadFile;
        }

        public TransmissionResult Call()
        {
            // Span span = tracer.newTrace().name("standalone").start();
            Trace span = zipkin4net.Trace.Create();
            span.Record(Annotations.ServiceName("standalone"));
            span.Record(Annotations.ClientSend());

            try
            {
                TransmissionResponse transmissionResponse;
                long duration = 0;

                if (this.parameters.UseFactory)
                {
                    using (Stream inputStream = File.Open(this.xmlPayloadFile.FullName, FileMode.Open, FileAccess.Read))
                    {
                        transmissionResponse = this.parameters.HyperwayOutboundComponent.getTransmissionService()
                            .send(inputStream, span);
                    }
                }
                else
                {

                    TransmissionRequest transmissionRequest = this.createTransmissionRequest(span);

                    Transmitter transmitter;
                    // Span span1 = tracer.newChild(span.context()).name("get transmitter").start();
                    Trace span1 = span.Child();
                    span1.Record(Annotations.ServiceName("get transmitter"));
                    span1.Record(Annotations.ClientSend());
                    try
                    {
                        transmitter = this.parameters.HyperwayOutboundComponent.getTransmitter();
                    }
                    finally
                    {
                        span1.Record(Annotations.ClientRecv());
                        // span1.finish();
                    }

                    // Performs the transmission
                    var watch = new Stopwatch();
                    // long start = System.nanoTime();
                    watch.Start();
                    transmissionResponse = this.performTransmission(
                        this.parameters.EvidencePath, transmitter, transmissionRequest, span);
                    watch.Stop();
                    
                    // long elapsed = System.nanoTime() - start;
                    // duration = TimeUnit.MILLISECONDS.convert(elapsed, TimeUnit.NANOSECONDS);

                    return new TransmissionResult(watch.ElapsedMilliseconds, transmissionResponse.getTransmissionIdentifier());
                }

                return new TransmissionResult(duration, transmissionResponse.getTransmissionIdentifier());
            }
            finally
            {
                // span.finish();
                span.Record(Annotations.ClientRecv());
            }
        }

        protected TransmissionRequest createTransmissionRequest(Trace root)
        {
            Trace span = root.Child();
            span.Record(Annotations.ServiceName("create transmission request"));
            span.Record(Annotations.ClientSend());

            // Span span = tracer.newChild(root.context()).name("create transmission request").start();
            try
            {
                // creates a transmission request builder and enables trace
                TransmissionRequestBuilder requestBuilder =
                    this.parameters.HyperwayOutboundComponent.getTransmissionRequestBuilder();

                requestBuilder.setTransmissionBuilderOverride(true);

                // add receiver participant
                if (this.parameters.Receiver != null)
                {
                    requestBuilder.receiver(this.parameters.Receiver);
                }

                // add sender participant
                if (this.parameters.Sender != null)
                {
                    requestBuilder.sender(this.parameters.Sender);
                }

                if (this.parameters.DocType != null)
                {
                    requestBuilder.documentType(this.parameters.DocType);
                }

                if (this.parameters.ProcessIdentifier != null)
                {
                    requestBuilder.processType(this.parameters.ProcessIdentifier);
                }

                // Supplies the payload
                using (Stream inputStream = File.Open(this.xmlPayloadFile.FullName, FileMode.Open, FileAccess.Read))
                {
                    requestBuilder.payLoad(inputStream);
                }

                // Overrides the destination URL if so requested
                if (this.parameters.Endpoint != null)
                {
                    Endpoint endpoint = this.parameters.Endpoint;
                    requestBuilder.overrideAs2Endpoint(endpoint);
                }

                // Specifying the details completed, creates the transmission request
                return requestBuilder.build(span);
            }
            catch (Exception e)
            {
                span.Record(Annotations.Tag("exception", e.Message));
                // span.tag("exception", String.valueOf(e.getMessage()));
                Console.WriteLine("");
                Console.WriteLine("Message failed : " + e.Message);
                //e.printStackTrace();
                Console.WriteLine("");
                return null;
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
                // span.finish();
            }
        }

        protected TransmissionResponse performTransmission(
            DirectoryInfo evidencePath,
            Transmitter transmitter,
            TransmissionRequest transmissionRequest,
            Trace root)
        {
            // Span span = tracer.newChild(root.context()).name("transmission").start();
            Trace span = root.Child();
            span.Record(Annotations.ServiceName("transmission"));
            span.Record(Annotations.ClientSend());
            try
            {
                // ... and performs the transmission
                Stopwatch watch = new Stopwatch();
                watch.Start();
                // long start = System.DateTime.Now;
                TransmissionResponse transmissionResponse = transmitter.transmit(transmissionRequest, span);
                watch.Stop();

                long durationInMs = watch.ElapsedMilliseconds; // System.DateTime.Now - start;

                // long durartionInMs = TimeUnit.MILLISECONDS.convert(elapsed, TimeUnit.NANOSECONDS);
                // Write the transmission id and where the message was delivered
                log.Debug(
                    String.Format(
                        "Message using messageId {0} sent to {1} using {2} was assigned transmissionId {3} took {4}ms\n",
                        transmissionResponse.getHeader().getIdentifier().getIdentifier(),
                        transmissionResponse.getEndpoint().getAddress(),
                        transmissionResponse.getProtocol().getIdentifier(),
                        transmissionResponse.getTransmissionIdentifier(),
                        durationInMs));

                this.saveEvidence(transmissionResponse, evidencePath, span);

                return transmissionResponse;
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
                // span.finish();
            }
        }

        protected void saveEvidence(
            TransmissionResponse transmissionResponse,
            DirectoryInfo evidencePath,
            Trace root) // throws IOException
        {
            // Span span = tracer.newChild(root.context()).name("save evidence").start();
            Trace span = Trace.Create();
            span.Record(Annotations.ServiceName("save evidence"));
            span.Record(Annotations.ClientSend());
            try
            {
                // saveEvidence(transmissionResponse, "-rem-evidence.xml",
                // transmissionResponse::getRemEvidenceBytes, evidencePath);
                this.saveEvidence(
                    transmissionResponse,
                    "-as2-mdn.txt",
                    transmissionResponse.getNativeEvidenceBytes(),
                    evidencePath);
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
                // span.finish();
            }
        }

        void saveEvidence(
            TransmissionResponse transmissionResponse,
            String suffix,
            byte[] supplier,
            DirectoryInfo evidencePath) // throws IOException
        {
            String fileName = FileUtils.filterString(transmissionResponse.getTransmissionIdentifier().ToString())
                              + suffix;
            FileInfo evidenceFile = new FileInfo(Path.Combine(evidencePath.FullName, fileName));
            File.WriteAllBytes(evidenceFile.FullName, supplier);
            // ByteStreams.copy(new ByteArrayInputStream(supplier.get()), new FileOutputStream(evidenceFile));
            log.InfoFormat("Evidence written to '{0}'.", evidenceFile.FullName);
        }
    }

}