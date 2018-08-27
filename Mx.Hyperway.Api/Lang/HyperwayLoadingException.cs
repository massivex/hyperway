namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class HyperwayLoadingException : HyperwayRuntimeException
    {

        public HyperwayLoadingException(string message)
            : base(message)
        {

        }

        public HyperwayLoadingException(string message, Exception cause)
            : base(message, cause)
        {

        }
    }
}