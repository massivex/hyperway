using System;
using System.Text;

namespace Mx.Peppol.Lookup.Util
{
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;
    using Mx.Tools.Encoding;

    using Org.BouncyCastle.Security;

    public class DynamicHostnameGenerator
    {
        private IBaseEncoding encoding;

        /// <summary>
        /// Prefix for generated hostname. 
        /// </summary>
        private string prefix;

        /// <summary>
        /// Base hostname for lookup. 
        /// </summary>
        private string hostname;

        /// <summary>
        /// Algorithm used for geneation of hostname. 
        /// </summary>
        private string digestAlgorithm;

        public DynamicHostnameGenerator(string prefix, string hostname, string digestAlgorithm)
        : this(prefix, hostname, digestAlgorithm, new Base16Encoding())
        {
           
        }

        public DynamicHostnameGenerator(string prefix, string hostname, string digestAlgorithm, IBaseEncoding encoding)
        {
            this.prefix = prefix;
            this.hostname = hostname;
            this.digestAlgorithm = digestAlgorithm;
            this.encoding = encoding;
        }

        public string Generate(ParticipantIdentifier participantIdentifier)
        {
            string receiverHash;
            try
            {
                // Create digest based on participant identifier.
                var utf8Identifier = Encoding.UTF8.GetBytes(participantIdentifier.Identifier);
                byte[] digest = DigestUtilities.CalculateDigest(this.digestAlgorithm, utf8Identifier);

                // Create hex of digest.
                 receiverHash = this.encoding.ToString(digest).ToLowerInvariant();
            }
            catch (Exception e)
            {
                throw new LookupException(e.Message, e);
            }

            return string.Format(
                "{0}{1}.{2}.{3}",
                this.prefix, receiverHash, participantIdentifier.Scheme.Identifier,
                this.hostname);
        }
    }
}
