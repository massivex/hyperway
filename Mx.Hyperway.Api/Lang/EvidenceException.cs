namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class EvidenceException : HyperwayException
    {

        public EvidenceException(String message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
