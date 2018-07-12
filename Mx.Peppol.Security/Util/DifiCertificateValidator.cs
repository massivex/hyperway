using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Security.Util
{
    using System.IO;

    using Mx.Certificates.Validator;
    using Mx.Certificates.Validator.Api;
    using Mx.Certificates.Validator.Lang;
    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Common.Lang;
    using Mx.Peppol.Mode;
    using Mx.Peppol.Security.Api;
    using Mx.Peppol.Security.Lang;

    using Org.BouncyCastle.X509;

    public class DifiCertificateValidator : CertificateValidator
    {

        private ValidatorGroup validator;

        private Mode mode;

        public DifiCertificateValidator(Mode mode) // throws PeppolLoadingException
        {
            this.mode = mode;

            try
            {
                var file = new FileInfo(mode.getString("security.pki"));
                this.validator = ValidatorLoader.newInstance().build(file);
            }
            catch (ValidatorParsingException e)
            {
                throw new PeppolLoadingException("Unable to initiate PKI.", e);
            }
        }


        public void validate(Service service, X509Certificate certificate) // throws PeppolSecurityException
        {
            try
            {
                this.validator.validate(
                    this.mode.getString(String.Format("security.validator.{0}", service.ToString())),
                    certificate);
            }
            catch (CertificateValidationException e)
            {
                throw new PeppolSecurityException(e.Message, e);
            }
        }
    }

}