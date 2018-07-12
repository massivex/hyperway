namespace Mx.Certificates.Validator.Xml
{
    using System;
    using System.Xml.Serialization;

    public class CertificateXml
    {
        [XmlText]
        public string Base64 { get; set; }

        public byte[] AsBuffer()
        {
            if (string.IsNullOrWhiteSpace(this.Base64))
            {
                return new byte[0];
            }

            return Convert.FromBase64String(this.Base64);
        }
    }
}