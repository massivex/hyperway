namespace Mx.Hyperway.Api.Lang
{
    using System;

    /**
     * Security exceptions are always thrown to indicate a certain action would involve stepping outside
     * current security domain, and forcing such action must be seen as a no-go.
     */
    public class HyperwaySecurityException : HyperwayException
    {

        public HyperwaySecurityException(String message)
            : base(message)
        {

        }

        public HyperwaySecurityException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
