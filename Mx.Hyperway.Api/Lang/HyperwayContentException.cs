namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class HyperwayContentException : HyperwayException
    {

        public HyperwayContentException(String message) 
            : base(message)
        {
        }

        public HyperwayContentException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
