using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Model
{
    using Mx.Oxalis.As2.Util;
    using Mx.Peppol.Common.Model;
    using Mx.Tools.Encoding;

    /**
     * Value object holding the Message Integrity Control (MIC) of an AS2 message.
     */
    public class Mic
    {

        private readonly String digestAsString;

        private readonly SMimeDigestMethod algorithm;

        public Mic(Digest digest) : this(new Base64Encoding().ToString(digest.getValue()),
            SMimeDigestMethod.findByDigestMethod(digest.getMethod()))
        {
            
        }

        public Mic(String digestAsString, SMimeDigestMethod algorithm)
        {
            this.digestAsString = digestAsString;
            this.algorithm = algorithm;
        }

        public static Mic valueOf(String receivedContentMic)
        {
            String[] s = receivedContentMic.Split(',');
            if (s.Length != 2)
            {
                throw new ArgumentException("Invalid mic: '" + receivedContentMic + "'. Required syntax: encoded-message-digest \",\" (sha1|md5)");
            }
            return new Mic(s[0].Trim(), SMimeDigestMethod.findByIdentifier(s[1].Trim()));
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}", digestAsString, algorithm.getIdentifier());
        }

        
        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is Mic)) return false;

            Mic mic = (Mic)o;

            if (!digestAsString.Equals(mic.digestAsString)) return false;
            return algorithm.Equals(mic.algorithm);
        }

        public override int GetHashCode()
        {
            int result = digestAsString.GetHashCode();
            result = 31 * result + algorithm.GetHashCode();
            return result;
        }
    }

}
