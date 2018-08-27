namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class HyperwayPluginException : HyperwayRuntimeException
    {

        public HyperwayPluginException(string message)
            : base(message)
        {

        }

        public HyperwayPluginException(string message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
