namespace Mx.Tools.Encoding
{
    public class Base16Encoding : IBaseEncoding
    {
        public string Encoding => "base16";

        public byte[] FromString(string inData)
        {
            return inData.FromBase16String();
        }

        public byte[] FromCharArray(char[] inData, int offset, int length)
        {
            return inData.FromBase16CharArray(offset, length);
        }

        public string ToString(byte[] inArray)
        {
            return inArray.ToBase16String();
        }

        public string ToString(byte[] inArray, int offset, int length)
        {
            return inArray.ToBase16String(offset, length);
        }
    }
}