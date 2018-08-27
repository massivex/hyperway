namespace Mx.Hyperway.Api.Lang
{
    using System;

    public abstract class HyperwayRuntimeException : Exception
    {

        public HyperwayRuntimeException(string message)
            : base(message)
        {

        }

        public HyperwayRuntimeException(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}