using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{

    public class Scheme : AbstractSimpleIdentifier
    {

        private static readonly long serialVersionUID = -6022267082161778285L;

        public static readonly Scheme NONE = of("NONE");

        public static Scheme of(String value)
        {
            return new Scheme(value);
        }

        protected Scheme(String value): base(value) { }
    }

}
