using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Security.Api
{
    using System.Security.Cryptography.X509Certificates;

    using Mx.Peppol.Common.Code;

    public interface CertificateValidator
    {

        void validate(Service service, X509Certificate certificate); //throws PeppolSecurityException;

    }

}
