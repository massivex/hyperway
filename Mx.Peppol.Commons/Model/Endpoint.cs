using System;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Api;

    using Org.BouncyCastle.X509;

    public class Endpoint : ISimpleEndpoint
    {
        public static Endpoint Of(TransportProfile transportProfile, Uri address, X509Certificate certificate)
        {
            return new Endpoint(transportProfile, address, certificate);
        }

        private Endpoint(TransportProfile transportProfile, Uri address, X509Certificate certificate)
        {
            this.TransportProfile = transportProfile;
            this.Address = address;
            this.Certificate = certificate;
        }

        public TransportProfile TransportProfile { get; }

        public Uri Address { get; }

        public X509Certificate Certificate { get; }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is Endpoint)) return false;

            Endpoint endpoint = (Endpoint)o;

            if (!this.TransportProfile.Equals(endpoint.TransportProfile)) return false;
            if (!this.Address.Equals(endpoint.Address)) return false;
            return !(!this.Certificate?.Equals(endpoint.Certificate) ?? endpoint.Certificate != null);

        }

        public override int GetHashCode()
        {
            int result = this.TransportProfile.GetHashCode();
            result = 31 * result + this.Address.GetHashCode();
            result = 31 * result + (this.Certificate != null ? this.Certificate.GetHashCode() : 0);
            return result;
        }

        public override string ToString()
        {
            return "Endpoint{" + "transportProfile=" + this.TransportProfile + ", address=" + this.Address
                   + ", certificate=" + this.Certificate + '}';
        }
    }
}
