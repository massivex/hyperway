﻿using System;

namespace Mx.Certificates.Validator.Rules
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Pkix;
    using Org.BouncyCastle.Security;
    using Org.BouncyCastle.Utilities.Collections;
    using Org.BouncyCastle.X509;
    using Org.BouncyCastle.X509.Store;

    /**
  * Validator checking validity of chain using root certificates and intermediate certificates.
  */
    public class ChainRule : ValidatorRule
    {

        private CertificateBucket rootCertificates;

        private CertificateBucket intermediateCertificates;

        private HashSet policies = new HashSet();

        /**
         * @param rootCertificates         Trusted root certificates.
         * @param intermediateCertificates Trusted intermediate certificates.
         */
        public ChainRule(
            CertificateBucket rootCertificates,
            CertificateBucket intermediateCertificates,
            String[] policies)
        {
            this.rootCertificates = rootCertificates;
            this.intermediateCertificates = intermediateCertificates;
            this.policies.AddAll(policies);
        }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            try
            {
                this.verifyCertificate(certificate);
            }
            catch (GeneralSecurityException e)
            {
                throw new FailedValidationException(e.Message, e);
            }
        }

        private PkixCertPathBuilderResult verifyCertificate(X509Certificate cert) // throws GeneralSecurityException
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
            // CertPathBuilder builder = CertPathBuilder.getInstance("PKIX");
            return builder.Build(pkixParams);
        }
    }
}

