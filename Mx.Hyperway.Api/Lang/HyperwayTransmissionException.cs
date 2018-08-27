namespace Mx.Hyperway.Api.Lang
{
    using System;

    /// <summary>
    /// Thrown when there is a problem related to the actual transmission protocol.</summary>
    public class HyperwayTransmissionException : HyperwayException
    {

        public HyperwayTransmissionException(string message)
            : base(message)
        {
        }

        public HyperwayTransmissionException(string message, Exception cause)
            : base(message, cause)
        {

        }

        public HyperwayTransmissionException(Uri url, Exception cause)
            : base($"Transmission failed to endpoint '{url}'.", cause)
        {

        }

        public HyperwayTransmissionException(string msg, Uri url, Exception e)
            : base($"{msg} - Transmission failed to endpoint '{url}' ", e)
        {

        }
    }
}
