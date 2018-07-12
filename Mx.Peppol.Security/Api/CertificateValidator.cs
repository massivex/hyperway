using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Security.Api
{
    using Mx.Peppol.Common.Code;

    using Org.BouncyCastle.X509;

    public interface CertificateValidator
    {

        void validate(Service service, X509Certificate certificate); //throws PeppolSecurityException;

    }

}
