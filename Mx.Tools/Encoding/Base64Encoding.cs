﻿namespace Mx.Tools.Encoding
{
    using System;

    public class Base64Encoding : IBaseEncoding
    {
        public string Encoding => "base64";

        public byte[] FromBytes(byte[] data)
        {
            return Convert.FromBase64String(data.ToBase64String());
        }

        public byte[] FromString(string inData)
        {
            return inData.FromBase64String();
        }

        public byte[] FromCharArray(char[] inData, int offset, int length)
        {
            return inData.FromBase64CharArray(offset, length);
        }

        public string ToString(byte[] inArray)
        {
            return inArray.ToBase64String();
        }

        public string ToString(byte[] inArray, int offset, int length)
        {
            return inArray.ToBase64String(offset, length);
        }
    }
}