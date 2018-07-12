using System;

namespace Mx.Peppol.Common.Model
{
    using System.Security.Cryptography.X509Certificates;

    using Mx.Peppol.Common.Api;

    public class Endpoint : SimpleEndpoint
    {

        private static readonly long serialVersionUID = 5892469135654700883L;

        private readonly TransportProfile transportProfile;

        private readonly Uri address;

        private readonly X509Certificate certificate;

        public static Endpoint of(TransportProfile transportProfile, Uri address, X509Certificate certificate)
        {
            return new Endpoint(transportProfile, address, certificate);
        }

        private Endpoint(TransportProfile transportProfile, Uri address, X509Certificate certificate)
        {
            this.transportProfile = transportProfile;
            this.address = address;
            this.certificate = certificate;
        }

        public TransportProfile getTransportProfile()
        {
            return transportProfile;
        }

        public Uri getAddress()
        {
            return address;
        }

        public X509Certificate getCertificate()
        {
            return certificate;
        }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is Endpoint)) return false;

            Endpoint endpoint = (Endpoint)o;

            if (!this.transportProfile.Equals(endpoint.transportProfile)) return false;
            if (!this.address.Equals(endpoint.address)) return false;
            return !(this.certificate != null
                         ? !this.certificate.Equals(endpoint.certificate)
                         : endpoint.certificate != null);

        }

        public override int GetHashCode()
        {
            int result = this.transportProfile.GetHashCode();
            result = 31 * result + this.address.GetHashCode();
            result = 31 * result + (this.certificate != null ? this.certificate.GetHashCode() : 0);
            return result;
        }

        public override String ToString()
        {
            return "Endpoint{" + "transportProfile=" + this.transportProfile + ", address=" + this.address
                   + ", certificate=" + this.certificate + '}';
        }
    }
}
