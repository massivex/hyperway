namespace Mx.Peppol.Security.Util
{
    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Security.Api;

    using Org.BouncyCastle.X509;

    public class EmptyCertificateValidator : ICertificateValidator
    {

        public static readonly ICertificateValidator Instance = new EmptyCertificateValidator();


        public void Validate(Service service, X509Certificate certificate)
        {
            // No action
        }
    }
}
