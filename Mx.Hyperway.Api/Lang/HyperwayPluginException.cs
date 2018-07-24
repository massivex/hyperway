namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class HyperwayPluginException : HyperwayRuntimeException
    {

        public HyperwayPluginException(String message)
            : base(message)
        {

        }

        public HyperwayPluginException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
