﻿using System;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Api;

    using Org.BouncyCastle.X509;

    public class Signed<T> : IPotentiallySigned<T>
    {
        public static Signed<T> Of(T content, X509Certificate certificate, DateTime? timestamp)
        {
            return new Signed<T>(content, certificate, timestamp);
        }

        public static Signed<T> Of(T content, X509Certificate certificate)
        {
            return Of(content, certificate, null);
        }

        private Signed(T content, X509Certificate certificate, DateTime? timestamp)
        {
            this.Content = content;
            this.Certificate = certificate;
            this.Timestamp = timestamp;
        }

        public T Content { get; }

        public IPotentiallySigned<TS> OfSubset<TS>(TS s)
        {
            return new Signed<TS>(s, this.Certificate, this.Timestamp);
        }

        public X509Certificate Certificate { get; }

        public DateTime? Timestamp { get; }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is Signed<T>)) return false;

            Signed<T> signed = (Signed<T>)o;

            if (!this.Content.Equals(signed.Content)) return false;
            if (!this.Certificate.Equals(signed.Certificate)) return false;
            return !(this.Timestamp != null ? !this.Timestamp.Equals(signed.Timestamp) : signed.Timestamp != null);

        }

        public override int GetHashCode()
        {
            int result = this.Content.GetHashCode();
            result = 31 * result + this.Certificate.GetHashCode();
            result = 31 * result + (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
            return result;
        }

        public override string ToString()
        {
            return $"Signed{{content={this.Content}, certificate={this.Certificate}, timestamp={this.Timestamp}{'}'}";
        }
    }

}
