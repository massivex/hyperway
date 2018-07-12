using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Standalone
{
    using Mx.Oxalis.Api.Model;

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
            return duration;
        }

        public TransmissionIdentifier getTransmissionIdentifier()
        {
            return transmissionIdentifier;
        }
    }

}
