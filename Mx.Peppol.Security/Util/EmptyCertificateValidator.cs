using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Security.Util
{
    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Security.Api;

    using Org.BouncyCastle.X509;

    public class EmptyCertificateValidator : CertificateValidator
    {

        public static readonly CertificateValidator INSTANCE = new EmptyCertificateValidator();


        public void validate(Service service, X509Certificate certificate) // throws PeppolSecurityException
        {
            // No action
        }
    }
}
