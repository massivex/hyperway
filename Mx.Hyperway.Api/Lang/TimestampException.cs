namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class TimestampException : HyperwayException
    {

        public TimestampException(string message)
            : base(message)
        {

        }

        public TimestampException(string message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
