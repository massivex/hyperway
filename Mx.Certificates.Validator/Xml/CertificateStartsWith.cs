using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class CertificateStartsWithType
    {
        [XmlAttribute("keyStore")]
        public string KeyStore { get; set; }
   
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
