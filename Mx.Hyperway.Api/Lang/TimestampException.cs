namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class TimestampException : HyperwayException
    {

        public TimestampException(String message)
            : base(message)
        {

        }

        public TimestampException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
