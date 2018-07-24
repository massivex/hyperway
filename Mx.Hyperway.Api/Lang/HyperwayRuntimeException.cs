namespace Mx.Hyperway.Api.Lang
{
    using System;

    public abstract class HyperwayRuntimeException : Exception
    {

        public HyperwayRuntimeException(String message)
            : base(message)
        {

        }

        public HyperwayRuntimeException(String message, Exception cause)
            : base(message, cause)
        {
        }
    }
}