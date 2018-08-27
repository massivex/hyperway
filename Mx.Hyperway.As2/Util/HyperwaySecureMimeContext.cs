namespace Mx.Hyperway.As2.Util
{
    using System.Linq;

    using MimeKit.Cryptography;

    using Org.BouncyCastle.Pkcs;

    public class HyperwaySecureMimeContext : DefaultSecureMimeContext
    {
        public HyperwaySecureMimeContext(
            Pkcs12Store store
            ) : base(new X509Context(store))
        {
            
        }
    }
}
