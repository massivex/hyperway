namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class HyperwayLoadingException : HyperwayRuntimeException
    {

        public HyperwayLoadingException(String message)
            : base(message)
        {

        }

        public HyperwayLoadingException(String message, Exception cause)
            : base(message)
        {

        }
    }
}