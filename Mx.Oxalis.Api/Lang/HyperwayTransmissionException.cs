namespace Mx.Hyperway.Api.Lang
{
    using System;

    /**
     * Thrown when there is a problem related to the actual transmission protocol.
     * <p>
     * Created by soc on 17.06.2016.
     */
    public class HyperwayTransmissionException : HyperwayException
    {

        public HyperwayTransmissionException(String message)
            : base(message)
        {
        }

        public HyperwayTransmissionException(String message, Exception cause)
            : base(message, cause)
        {

        }

        public HyperwayTransmissionException(Uri url, Exception cause)
            : base($"Transmission failed to endpoint '{url}'.", cause)
        {

        }

        public HyperwayTransmissionException(String msg, Uri url, Exception e)
            : base($"{msg} - Transmission failed to endpoint '{url}' ", e)
        {

        }
    }
}
