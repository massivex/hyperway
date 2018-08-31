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

    public class DifiCertificateValidator : ICertificateValidator
    {
        private readonly ValidatorGroup validator;

        private readonly Mode mode;

        public DifiCertificateValidator(Mode mode)
        {
            this.mode = mode;

            try
            {
                var file = new FileInfo(mode.GetValue("security.pki"));
                this.validator = ValidatorLoader.NewInstance().Build(file);
            }
            catch (ValidatorParsingException e)
            {
                throw new PeppolLoadingException("Unable to initiate PKI.", e);
            }
        }


        public void Validate(Service service, X509Certificate certificate)
        {
            try
            {
                var key = $"security.validator.{service.ToString().ToUpperInvariant()}";
                this.validator.Validate(this.mode.GetValue(key), certificate);
            }
            catch (CertificateValidationException e)
            {
                throw new PeppolSecurityException(e.Message, e);
            }
        }
    }

}