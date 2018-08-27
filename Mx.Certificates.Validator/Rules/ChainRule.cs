namespace Mx.Certificates.Validator.Rules
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Pkix;
    using Org.BouncyCastle.Security;
    using Org.BouncyCastle.Utilities.Collections;
    using Org.BouncyCastle.X509;
    using Org.BouncyCastle.X509.Store;

    /// <summary>
    /// Validator checking validity of chain using root certificates and intermediate certificates.
    /// </summary>
    public class ChainRule : IValidatorRule
    {

        private readonly ICertificateBucket rootCertificates;

        private readonly ICertificateBucket intermediateCertificates;

        private readonly HashSet policies = new HashSet();

        /// <summary>
        /// Create a new <see cref="ChainRule"/> instance"/>
        /// </summary>
        /// <param name="rootCertificates">Trusted root certificates</param>
        /// <param name="intermediateCertificates">Trusted intermediate certificates</param>
        /// <param name="policies"></param>
        public ChainRule(
            ICertificateBucket rootCertificates,
            ICertificateBucket intermediateCertificates,
            string[] policies)
        {
            this.rootCertificates = rootCertificates;
            this.intermediateCertificates = intermediateCertificates;
            this.policies.AddAll(policies);
        }

        public void Validate(X509Certificate certificate)
        {
            try
            {
                this.VerifyCertificate(certificate);
            }
            catch (GeneralSecurityException e)
            {
                throw new FailedValidationException(e.Message, e);
            }
        }

        private void VerifyCertificate(X509Certificate cert)
        {
            // Create the selector that specifies the starting certificate
            X509CertStoreSelector selector = new X509CertStoreSelector();
            selector.Certificate = cert;

            // Create the trust anchors (set of root CA certificates)
            HashSet trustAnchors = new HashSet();
            foreach (X509Certificate trustedRootCert in this.rootCertificates)
            {
                trustAnchors.Add(new TrustAnchor(trustedRootCert, null));
            }

            // Configure the PKIX certificate builder algorithm parameters
            PkixBuilderParameters pkixParams = new PkixBuilderParameters(trustAnchors, selector);

            // Setting explicit policy
            if (this.policies.Count > 0)
            {
                pkixParams.SetInitialPolicies(this.policies);
                pkixParams.IsExplicitPolicyRequired = true;
            }

            // Disable CRL checks (this is done manually as additional step)
            pkixParams.IsRevocationEnabled = false;

            // Specify a list of intermediate certificates
            HashSet trustedIntermediateCert = new HashSet();
            foreach (X509Certificate certificate in this.intermediateCertificates)
            {
                trustedIntermediateCert.Add(certificate);
            }

            var store = X509StoreFactory.Create(
                "Collection",
                new X509CollectionStoreParameters(trustedIntermediateCert));
            pkixParams.AddStore(store);

            // Build and verify the certification chain
            PkixCertPathBuilder builder = new PkixCertPathBuilder();
            builder.Build(pkixParams);
        }
    }
}

