﻿namespace Mx.Tools
{
    public static class ByteExtensions
    {
        public static string ToUtf8String(this byte[] data)
        {
            return System.Text.Encoding.UTF8.GetString(data);
        }
    }
}
