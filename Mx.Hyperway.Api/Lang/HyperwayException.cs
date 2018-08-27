namespace Mx.Hyperway.Api.Lang
{
    using System;

    public abstract class HyperwayException : Exception
    {

        public HyperwayException(string message) : base(message)
        {

        }

        public HyperwayException(string message, Exception cause) : base(message, cause)
        {

        }
    }
}
