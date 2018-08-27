﻿namespace Mx.Hyperway.As2.Lang
{
    using System;

    using Mx.Hyperway.Api.Lang;

    public class HyperwayAs2Exception : HyperwayException
    {

        public HyperwayAs2Exception(string message)
            : base(message)
        {
        }

        public HyperwayAs2Exception(string message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
