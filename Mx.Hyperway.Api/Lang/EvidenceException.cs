namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class EvidenceException : HyperwayException
    {

        public EvidenceException(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
