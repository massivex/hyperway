using System.Collections.Generic;

namespace Mx.Certificates.Validator.Util
{
    using System.Linq;

    using Mx.Certificates.Validator.Api;

    /// <summary>
    /// Validate principal name using a static list of values. 
    /// </summary>
    public class SimplePrincipalNameProvider : IPrincipalNameProvider<string>
    {

        private readonly List<string> expected;

        public SimplePrincipalNameProvider(params string[] expected)
            : this(expected.AsEnumerable())
        {

        }

        public SimplePrincipalNameProvider(IEnumerable<string> expected)
        {
            this.expected = expected.ToList();
        }

        public bool Validate(string value)
        {
            return this.expected.Contains(value);
        }
    }

}
