namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class HyperwayContentException : HyperwayException
    {

        public HyperwayContentException(string message) 
            : base(message)
        {
        }

        public HyperwayContentException(string message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
