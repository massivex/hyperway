namespace Mx.Hyperway.Api.Lang
{
    using System;

    /// <summary>
    /// Security exceptions are always thrown to indicate a certain action would involve stepping outside
    /// current security domain, and forcing such action must be seen as a no-go.
    /// </summary>
    public class HyperwaySecurityException : HyperwayException
    {

        public HyperwaySecurityException(string message)
            : base(message)
        {

        }

        public HyperwaySecurityException(string message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
