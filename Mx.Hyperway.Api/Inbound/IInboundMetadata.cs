namespace Mx.Hyperway.Api.Inbound
{
    using Mx.Hyperway.Api.Transmission;

    using Org.BouncyCastle.X509;

    public interface IInboundMetadata : ITransmissionResult
    {
        /// <summary>
        /// Fetch sender's certificate. 
        /// </summary>
        /// <returns></returns>
        X509Certificate GetCertificate();
    }
}
