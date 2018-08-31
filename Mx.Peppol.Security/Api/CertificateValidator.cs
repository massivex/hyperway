namespace Mx.Peppol.Security.Api
{
    using Mx.Peppol.Common.Code;

    using Org.BouncyCastle.X509;

    public interface ICertificateValidator
    {
        void Validate(Service service, X509Certificate certificate);
    }
}
