using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Util
{
    using ARSoft.Tools.Net;

    using Mx.Tools.Encoding;

    public enum BaseEncodingType
    {
        Base16,
        Base32,
        Base64
    }


    public class EncodingUtils
    {

        public static IBaseEncoding get(BaseEncodingType type)
        {
            switch (type)
            {
                case BaseEncodingType.Base64:
                    return new Base64Encoding();
                
                case BaseEncodingType.Base32:
                    return new Base32Encoding();

                case BaseEncodingType.Base16:
                    return new Base16Encoding();

                default:
                    return null;
            }
        }

        public static IBaseEncoding get(String identifier)
        {
            switch (identifier)
            {
                case "base64":
                    return new Base64Encoding();
                case "base32":
                    return new Base32Encoding();
                case "base16":
                    return new Base16Encoding();
                default:
                    return null;
            }
        }

    }
}
