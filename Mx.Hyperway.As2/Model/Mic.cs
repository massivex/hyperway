namespace Mx.Hyperway.As2.Model
{
    using System;

    using Mx.Hyperway.As2.Util;
    using Mx.Peppol.Common.Model;
    using Mx.Tools.Encoding;

    /// <summary>
    /// Value object holding the Message Integrity Control (MIC) of an AS2 message. 
    /// </summary>
    public class Mic
    {

        private readonly String digestAsString;

        private readonly SMimeDigestMethod algorithm;

        public Mic(Digest digest) : this(new Base64Encoding().ToString(digest.Value),
            SMimeDigestMethod.FindByDigestMethod(digest.Method))
        {
            
        }

        public Mic(String digestAsString, SMimeDigestMethod algorithm)
        {
            this.digestAsString = digestAsString;
            this.algorithm = algorithm;
        }

        public static Mic ValueOf(String receivedContentMic)
        {
            String[] s = receivedContentMic.Split(',');
            if (s.Length != 2)
            {
                throw new ArgumentException("Invalid mic: '" + receivedContentMic + "'. Required syntax: encoded-message-digest \",\" (sha1|md5)");
            }
            return new Mic(s[0].Trim(), SMimeDigestMethod.FindByIdentifier(s[1].Trim()));
        }

        public override string ToString()
        {
            return $"{this.digestAsString}, {this.algorithm.GetIdentifier()}";
        }

        
        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is Mic)) return false;

            Mic mic = (Mic)o;

            if (!this.digestAsString.Equals(mic.digestAsString)) return false;
            return this.algorithm.Equals(mic.algorithm);
        }

        public override int GetHashCode()
        {
            int result = this.digestAsString.GetHashCode();
            result = 31 * result + this.algorithm.GetHashCode();
            return result;
        }
    }

}
