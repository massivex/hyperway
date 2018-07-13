namespace Mx.Tools.Encoding
{
    public interface IBaseEncoding
    {
        string Encoding { get; }

        byte[] FromString(string inData);

        byte[] FromCharArray(char[] inData, int offset, int length);

        string ToString(byte[] inArray);

        string ToString(byte[] inArray, int offset, int length);
    }
}

