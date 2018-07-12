namespace Mx.Certificates.Validator.Xml
{
    using System;
    using System.Xml.Serialization;

    public class CertificateType
    {
        /// <summary>
        /// Base64 certificate content
        /// </summary>
        [XmlText]
        public string Value { get; set; }

        public byte[] AsBuffer()
        {
            if (string.IsNullOrWhiteSpace(this.Value))
            {
                return new byte[0];
            }

            return Convert.FromBase64String(this.Value);
        }
    }
}