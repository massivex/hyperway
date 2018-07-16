using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Api
{
    public class NamespaceAttribute : Attribute {
        public NamespaceAttribute(string value)
        {
            this.Value = value;
        }

        public string Value { get; }
    }
}
