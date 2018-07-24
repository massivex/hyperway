namespace Mx.Hyperway.Api.Lang
{
    using System;

    public abstract class HyperwayException : Exception
    {

        public HyperwayException(String message)
            : base(message)
        {

        }

        public HyperwayException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
