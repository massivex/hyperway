using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Util
{
    /**
    * From<a href = "https://tools.ietf.org/html/rfc5280#section-4.2.1.3" > RFC5280 4.2.1.3</a>.
    *
    * @author erlend
    */
    public enum KeyUsage
    {
        DIGITAL_SIGNATURE = 0,

        NON_REPUDIATION = 1,

        KEY_ENCIPHERMENT = 2,

        DATA_ENCIPHERMENT = 4,

        KEY_AGREEMENT = 8,

        KEY_CERT_SIGN = 16,

        CRL_SIGN = 32,

        ENCIPHER_ONLY = 64,

        DECIPHER_ONLY = 128
    }

//    private final int bit;

//        public static KeyUsage of(int bit)
//        {
//        for (KeyUsage keyUsage : values())
//        if (keyUsage.bit == bit)
//        return keyUsage;

//        throw new IllegalArgumentException(String.format("Bit '%s' is not known.", bit));
//    }

//    KeyUsage(int bit)
//    {
//    this.bit = bit;
//}

//public int getBit()
//{
//return bit;
//}
//}

//    }
}
