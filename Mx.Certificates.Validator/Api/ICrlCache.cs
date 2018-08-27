namespace Mx.Certificates.Validator.Api
{
    using Org.BouncyCastle.X509;

    public interface ICrlCache : ICrlFetcher
    {
        void Set(string url, X509Crl crl);
    }
}
