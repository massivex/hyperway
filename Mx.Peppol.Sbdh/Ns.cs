using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Sbdh
{
    using Mx.Tools;

    public class Ns
    {

        public static readonly String SBDH = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader";

        public static readonly String EXTENSION = "http://peppol.eu/xsd/ticc/envelope/1.0";

        public static readonly QName QNAME_BINARY_CONTENT = new QName(EXTENSION, "BinaryContent");

        public static readonly QName QNAME_TEXT_CONTENT = new QName(EXTENSION, "TextContent");

        public static readonly QName QNAME_SBD = new QName(SBDH, "StandardBusinessDocument");

        public static readonly QName QNAME_SBDH = new QName(SBDH, "StandardBusinessDocumentHeader");

    }
}
