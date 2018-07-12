using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lang
{
    /**
     * Thrown when there is a problem related to the actual transmission protocol.
     * <p>
     * Created by soc on 17.06.2016.
     */
    public class OxalisTransmissionException : OxalisException
    {

        public OxalisTransmissionException(String message)
            : base(message)
        {
        }

        public OxalisTransmissionException(String message, Exception cause)
            : base(message, cause)
        {

        }

        public OxalisTransmissionException(Uri url, Exception cause)
            : base($"Transmission failed to endpoint '{url}'.", cause)
        {

        }

        public OxalisTransmissionException(String msg, Uri url, Exception e)
            : base($"{msg} - Transmission failed to endpoint '{url}' ", e)
        {

        }
    }
}
