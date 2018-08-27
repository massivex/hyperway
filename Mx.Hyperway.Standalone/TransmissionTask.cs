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

        public static readonly ILog Log = LogManager.GetLogger(typeof(TransmissionTask));

        private readonly TransmissionParameters parameters;

        private readonly FileInfo xmlPayloadFile;

        public TransmissionTask(TransmissionParameters parameters, FileInfo xmlPayloadFile)
        {
            this.parameters = parameters;
            this.xmlPayloadFile = xmlPayloadFile;
        }

        public TransmissionResult Call()
        {
            Trace span = Trace.Create();
            span.Record(Annotations.ServiceName("standalone"));
            span.Record(Annotations.ClientSend());

            try
            {
                ITransmissionResponse transmissionResponse;
                long duration = 0;

                if (this.parameters.UseFactory)
                {
                    using (Stream inputStream = File.Open(this.xmlPayloadFile.FullName, FileMode.Open, FileAccess.Read))
                    {
                        transmissionResponse = this.parameters.HyperwayOutboundComponent.GetTransmissionService()
                            .Send(inputStream, span);
                    }
                }
                else
                {

                    ITransmissionRequest transmissionRequest = this.CreateTransmissionRequest(span);

                    ITransmitter transmitter;
                    Trace span1 = span.Child();
                    span1.Record(Annotations.ServiceName("get transmitter"));
                    span1.Record(Annotations.ClientSend());
                    try
                    {
                        transmitter = this.parameters.HyperwayOutboundComponent.GetTransmitter();
                    }
                    finally
                    {
                        span1.Record(Annotations.ClientRecv());
                    }

                    // Performs the transmission
                    var watch = new Stopwatch();
                    watch.Start();
                    transmissionResponse = this.PerformTransmission(
                        this.parameters.EvidencePath, transmitter, transmissionRequest, span);
                    watch.Stop();
                    
                    return new TransmissionResult(watch.ElapsedMilliseconds, transmissionResponse.GetTransmissionIdentifier());
                }

                return new TransmissionResult(duration, transmissionResponse.GetTransmissionIdentifier());
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }

        protected ITransmissionRequest CreateTransmissionRequest(Trace root)
        {
            Trace span = root.Child();
            span.Record(Annotations.ServiceName("create transmission request"));
            span.Record(Annotations.ClientSend());

            try
            {
                // creates a transmission request builder and enables trace
                TransmissionRequestBuilder requestBuilder =
                    this.parameters.HyperwayOutboundComponent.GetTransmissionRequestBuilder();

                requestBuilder.SetTransmissionBuilderOverride(true);

                // add receiver participant
                if (this.parameters.Receiver != null)
                {
                    requestBuilder.Receiver(this.parameters.Receiver);
                }

                // add sender participant
                if (this.parameters.Sender != null)
                {
                    requestBuilder.Sender(this.parameters.Sender);
                }

                if (this.parameters.DocType != null)
                {
                    requestBuilder.DocumentType(this.parameters.DocType);
                }

                if (this.parameters.ProcessIdentifier != null)
                {
                    requestBuilder.ProcessType(this.parameters.ProcessIdentifier);
                }

                // Supplies the payload
                using (Stream inputStream = File.Open(this.xmlPayloadFile.FullName, FileMode.Open, FileAccess.Read))
                {
                    requestBuilder.PayLoad(inputStream);
                }

                // Overrides the destination URL if so requested
                if (this.parameters.Endpoint != null)
                {
                    Endpoint endpoint = this.parameters.Endpoint;
                    requestBuilder.OverrideAs2Endpoint(endpoint);
                }

                // Specifying the details completed, creates the transmission request
                return requestBuilder.Build(span);
            }
            catch (Exception e)
            {
                span.Record(Annotations.Tag("exception", e.Message));
                Console.WriteLine("Message failed : " + e.Message + e.StackTrace);
                return null;
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }

        protected ITransmissionResponse PerformTransmission(
            DirectoryInfo evidencePath,
            ITransmitter transmitter,
            ITransmissionRequest transmissionRequest,
            Trace root)
        {
            Trace span = root.Child();
            span.Record(Annotations.ServiceName("transmission"));
            span.Record(Annotations.ClientSend());
            try
            {
                // ... and performs the transmission
                Stopwatch watch = new Stopwatch();
                watch.Start();
                // long start = System.DateTime.Now;
                ITransmissionResponse transmissionResponse = transmitter.Transmit(transmissionRequest, span);
                watch.Stop();

                long durationInMs = watch.ElapsedMilliseconds; // System.DateTime.Now - start;

                Log.Debug(
                    String.Format(
                        "Message using messageId {0} sent to {1} using {2} was assigned transmissionId {3} took {4}ms\n",
                        transmissionResponse.GetHeader().getIdentifier().getIdentifier(),
                        transmissionResponse.GetEndpoint().getAddress(),
                        transmissionResponse.GetProtocol().getIdentifier(),
                        transmissionResponse.GetTransmissionIdentifier(),
                        durationInMs));

                this.SaveEvidence(transmissionResponse, evidencePath, span);

                return transmissionResponse;
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }

        protected void SaveEvidence(
            ITransmissionResponse transmissionResponse,
            DirectoryInfo evidencePath,
            Trace root)
        {
            Trace span = Trace.Create();
            span.Record(Annotations.ServiceName("save evidence"));
            span.Record(Annotations.ClientSend());
            try
            {
                this.SaveEvidence(
                    transmissionResponse,
                    "-as2-mdn.txt",
#pragma warning disable 612
                    transmissionResponse.GetNativeEvidenceBytes(),
#pragma warning restore 612
                    evidencePath);
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }

        void SaveEvidence(
            ITransmissionResponse transmissionResponse,
            String suffix,
            byte[] supplier,
            DirectoryInfo evidencePath)
        {
            String fileName = FileUtils.filterString(transmissionResponse.GetTransmissionIdentifier().ToString())
                              + suffix;
            FileInfo evidenceFile = new FileInfo(Path.Combine(evidencePath.FullName, fileName));
            File.WriteAllBytes(evidenceFile.FullName, supplier);
            Log.InfoFormat("Evidence written to '{0}'.", evidenceFile.FullName);
        }
    }

}