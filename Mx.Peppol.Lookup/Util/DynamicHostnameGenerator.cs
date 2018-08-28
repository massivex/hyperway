using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Util
{
    using System.Security.Cryptography;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;
    using Mx.Tools.Encoding;

    using Org.BouncyCastle.Security;

    public class DynamicHostnameGenerator
    {

        // TODO: Check IoC in static constructor
        //static DynamicHostnameGenerator()
        //{
        //    // Make sure to register Bouncy Castle as a provider.
        //    if (Security.getProvider(BouncyCastleProvider.PROVIDER_NAME) == null)
        //        Security.addProvider(new BouncyCastleProvider());
        //}

        private IBaseEncoding encoding;

        /**
         * Prefix for generated hostname.
         */
        private String prefix;

        /**
         * Base hostname for lookup.
         */
        private String hostname;

        /**
         * Algorithm used for geneation of hostname.
         */
        private String digestAlgorithm;

        public DynamicHostnameGenerator(String prefix, String hostname, String digestAlgorithm)
        : this(prefix, hostname, digestAlgorithm, new Base16Encoding())
        {
           
        }

        public DynamicHostnameGenerator(String prefix, String hostname, String digestAlgorithm, IBaseEncoding encoding)
        {
            this.prefix = prefix;
            this.hostname = hostname;
            this.digestAlgorithm = digestAlgorithm;
            this.encoding = encoding;
        }

        public String generate(ParticipantIdentifier participantIdentifier) // throws LookupException
        {
            string receiverHash;
            try
            {
                // Create digest based on participant identifier.
                var utf8Identifier = Encoding.UTF8.GetBytes(participantIdentifier.Identifier);
                byte[] digest = DigestUtilities.CalculateDigest(this.digestAlgorithm, utf8Identifier);

                // Create hex of digest.
                 receiverHash = encoding.ToString(digest).ToLowerInvariant();
            }
            catch (Exception e)
            {
                throw new LookupException(e.Message, e);
            }

            return String.Format(
                "{0}{1}.{2}.{3}",
                this.prefix, receiverHash, participantIdentifier.Scheme.Identifier, hostname);
        }
    }
}
