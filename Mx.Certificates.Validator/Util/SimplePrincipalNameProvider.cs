using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Util
{
    using System.Linq;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Utilities;

    /**
     * Validate principal name using a static list of values.
     */
    public class SimplePrincipalNameProvider : PrincipalNameProvider<String>
    {

        private List<String> expected;

        public SimplePrincipalNameProvider(params String[] expected)
            : this(expected.AsEnumerable())
        {

        }

        public SimplePrincipalNameProvider(IEnumerable<String> expected)
        {
            this.expected = expected.ToList();
        }

        public bool validate(String value)
        {
            return expected.Contains(value);
        }
    }

}
