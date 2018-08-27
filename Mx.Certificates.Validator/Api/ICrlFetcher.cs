namespace Mx.Certificates.Validator.Api
{
    using Org.BouncyCastle.X509;

    public interface ICrlFetcher
    {
        X509Crl Get(string url);
    }
}
