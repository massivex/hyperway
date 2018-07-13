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

    public class Base64Encoding : IBaseEncoding
    {

        /// <summary>
        ///   Decodes a Base16 string as described in <see cref="!:http://tools.ietf.org/html/rfc4648">RFC 4648</see> .
        /// </summary>
        /// <param name="inData"> An Base16 encoded string. </param>
        /// <returns> Decoded data </returns>
        public static byte[] FromBase16String(this string inData)
        {
            return inData.ToCharArray().FromBase16CharArray(0, inData.Length);
        }

        /// <summary>
        ///   Decodes a Base16 char array as described in <see cref="!:http://tools.ietf.org/html/rfc4648">RFC 4648</see> .
        /// </summary>
        /// <param name="inData"> An Base16 encoded char array. </param>
        /// <param name="offset"> An offset in inData. </param>
        /// <param name="length"> The number of elements of inData to decode. </param>
        /// <returns> Decoded data </returns>
        public static byte[] FromBase16CharArray(this char[] inData, int offset, int length)
        {
            byte[] res = new byte[length / 2];

            int inPos = offset;
            int outPos = 0;

            while (inPos < offset + length)
            {
                res[outPos++] = (byte)((_base16ReverseAlphabet[inData[inPos++]] << 4) + _base16ReverseAlphabet[inData[inPos++]]);
            }

            return res;
        }
    }

}
