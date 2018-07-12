using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Util
{
    using System.Security.Cryptography;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;

    public class DynamicHostnameGenerator
    {

        // TODO: Check IoC in static constructor
        //static DynamicHostnameGenerator()
        //{
        //    // Make sure to register Bouncy Castle as a provider.
        //    if (Security.getProvider(BouncyCastleProvider.PROVIDER_NAME) == null)
        //        Security.addProvider(new BouncyCastleProvider());
        //}

        private Encoding encoding;

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
        {
            this.prefix = prefix;
            this.hostname = hostname;
            this.digestAlgorithm = digestAlgorithm;

            // this(prefix, hostname, digestAlgorithm, Encoding.base16());
        }

        // TODO: support more encodings
        //private DynamicHostnameGenerator(String prefix, String hostname, String digestAlgorithm, Encoding encoding)
        //{
        //    this.prefix = prefix;
        //    this.hostname = hostname;
        //    this.digestAlgorithm = digestAlgorithm;
        //    this.encoding = encoding;
        //}

        public String generate(ParticipantIdentifier participantIdentifier) // throws LookupException
        {
            string receiverHash;
            try
            {
                var sha1 = SHA1.Create();
                byte[] inputBytes = Encoding.ASCII.GetBytes(participantIdentifier.getIdentifier());
                byte[] outputBytes = sha1.ComputeHash(inputBytes);
                receiverHash = BitConverter.ToString(outputBytes).ToLowerInvariant().Replace("-", "").ToLower();
                //// Create digest based on participant identifier.
                //MessageDigest md = MessageDigest.getInstance(digestAlgorithm, BouncyCastleProvider.PROVIDER_NAME);
                //byte[] digest = md.digest(participantIdentifier.getIdentifier().getBytes(StandardCharsets.UTF_8));

                // Create hex of digest.
                // receiverHash = encoding.encode(digest).toLowerCase();
            }
            catch (Exception e)
            {
                throw new LookupException(e.Message, e);
            }

            return String.Format(
                "{0}{1}.{2}.{3}",
                this.prefix, receiverHash, participantIdentifier.getScheme().getIdentifier(), hostname);
        }
    }
}
