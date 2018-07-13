namespace Mx.Tools.Encoding
{
    public class Base32Encoding : IBaseEncoding
    {
        public string Encoding => "base32";

        public byte[] FromString(string inData)
        {
            return inData.FromBase32String();
        }

        public byte[] FromCharArray(char[] inData, int offset, int length)
        {
            return inData.FromBase32CharArray(offset, length);
        }

        public string ToString(byte[] inArray)
        {
            return inArray.ToBase32String();
        }

        public string ToString(byte[] inArray, int offset, int length)
        {
            return inArray.ToBase32String(offset, length);
        }
    }
}