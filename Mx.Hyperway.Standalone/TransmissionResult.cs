namespace Mx.Hyperway.Standalone
{
    using Mx.Hyperway.Api.Model;

    public class TransmissionResult
    {

        private readonly long duration;

        private readonly TransmissionIdentifier transmissionIdentifier;


        public TransmissionResult(long duration, TransmissionIdentifier transmissionIdentifier)
        {
            this.duration = duration;
            this.transmissionIdentifier = transmissionIdentifier;
        }

        public long getDuration()
        {
            return this.duration;
        }

        public TransmissionIdentifier getTransmissionIdentifier()
        {
            return this.transmissionIdentifier;
        }
    }

}
