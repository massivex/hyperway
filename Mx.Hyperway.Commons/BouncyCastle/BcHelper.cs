namespace Mx.Hyperway.Commons.BouncyCastle
{
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Security;

    public class BcHelper
    {
        public static byte[] Hash(byte[] content, string algorithm)
        {
            IDigest hasher = DigestUtilities.GetDigest(algorithm);
            byte[] result = new byte[hasher.GetDigestSize()];
            hasher.BlockUpdate(content, 0, content.Length);
            hasher.DoFinal(result, 0);
            return result;
        }

        //public static MessageDigest getMessageDigest(string algorithm) //throws NoSuchAlgorithmException
        //{
        //    try
        //    {
        //        return MessageDigest.getInstance(algorithm, BouncyCastleProvider.PROVIDER_NAME);
        //    }
        //    catch (NoSuchProviderException e)
        //    {
        //        throw new NoSuchAlgorithmException(e.getMessage(), e);
        //    }
        //}
    }
}
