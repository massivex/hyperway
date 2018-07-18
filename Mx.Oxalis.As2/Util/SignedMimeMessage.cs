using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Util
{
    using System.Collections;
    using System.IO;
    using System.Linq;

    using log4net;

    using MimeKit;

    using Mx.Oxalis.As2.Model;
    using Mx.Oxalis.Commons.BouncyCastle;
    using Mx.Tools.Encoding;

    using Org.BouncyCastle.Cms;
    using Org.BouncyCastle.Crypto.Paddings;
    using Org.BouncyCastle.Security.Certificates;
    using Org.BouncyCastle.X509.Store;

    using MimeMessage = MimeKit.MimeMessage;
    using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

    /**
     * Represents an S/MIME message, which provides meta information and data from the signed MimeMessage.
     *
     */
    public class SignedMimeMessage
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(SignedMimeMessage));

        private readonly MimeMessage mimeMessage;

        private X509Certificate signersX509Certificate;

        //    static {
        //    // Installs the Bouncy Castle provider
        //    BCHelper.registerProvider();
        //}

        public SignedMimeMessage(MimeMessage mimeMessage)
        {
            this.mimeMessage = mimeMessage;
            verifyContentType();

            parseSignedMessage();
        }


        /**
         * Provides an InputStream referencing the payload of the S/MIME message.
         * This includes the entire payload, including the SBDH.
         *
         * @return inputStream pointing to the first byte of the payload.
         */
        //public Stream getPayload()
        //{
        //    try
        //    {
        //        MimeMultipart mimeMultipart = (MimeMultipart)mimeMessage.getContent();
        //        BodyPart
        //            bodyPart = mimeMultipart
        //                .getBodyPart(0); // First part contains the data, second contains the signature
        //        return bodyPart.getInputStream();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new InvalidOperationException(
        //            "Unable to access the contents of the payload in first body part. " + e.Message,
        //            e);
        //    }
        //}


        public MimeMessage getMimeMessage()
        {
            return mimeMessage;
        }

        public X509Certificate getSignersX509Certificate()
        {
            return signersX509Certificate;
        }

        public Mic calculateMic(SMimeDigestMethod algorithm)
        {
            try
            {

                // MessageDigest messageDigest = BcHelper.getMessageDigest(algorithm.getAlgorithm());

                List<MimeEntity> mimeMultipart = mimeMessage.BodyParts.ToList();

                MimeEntity bodyPart = mimeMultipart[0];

                // ByteArrayOutputStream baos = new ByteArrayOutputStream();
                // bodyPart.writeTo(baos); // Writes the entire contents of first multipart, including the MIME headers
                byte[] content;
                using (var m = new MemoryStream())
                {
                    bodyPart.WriteTo(m, true);
                    content = m.ToArray();
                }
                //byte[] content = bodyPart.WriteTo(); //.toByteArray();
                // messageDigest.update(content);
                //String digestAsString = new String(Base64.encode(messageDigest.digest()));
                var digest = BcHelper.Hash(content, algorithm.getAlgorithm());
                var digestString = (new Base64Encoding()).ToString(digest);

                return new Mic(digestString, algorithm);

            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to handle mime body part. " + e.Message, e);
            }
        }

        /**
         * Calculates the message digest of the payload
         *
         * @return the Message digest for the payload
         */
        //public MessageDigestResult calcPayloadDigest(String algorithmName)
        //{

        //    MessageDigest instance;
        //    try
        //    {
        //        instance = MessageDigest.getInstance(algorithmName);
        //    }
        //    catch (NoSuchAlgorithmException e)
        //    {
        //        throw new IllegalStateException("Unable to create message digester " + e.getMessage(), e);
        //    }
        //    DigestInputStream digestInputStream = new DigestInputStream(getPayload(), instance);
        //    try
        //    {
        //        ByteStreams.exhaust(digestInputStream);
        //    }
        //    catch (IOException e)
        //    {
        //        throw new IllegalStateException("Error while reading Mime message payload for calculating digest." + e.getMessage(), e);
        //    }

        //    return new MessageDigestResult(instance.digest(), instance.getAlgorithm());
        //}


        private void verifyContentType()
        {
            try
            {
                // TODO: Fix LOG
                // at this stage we should have a javax.mail.internet.MimeMessage with content type text/plain
                // log.Debug("Verifying " + mimeMessage.GetType().Name + " with content type " + mimeMessage.GetContentType());

                // the contents of this should be a multipart MimeMultipart that is signed
                List<MimeEntity> bodies = this.mimeMessage.BodyParts.ToList();
                String contentType = this.mimeMessage.Headers[HeaderId.ContentType];

                if (!contentType.StartsWith("multipart/signed"))
                {
                    throw new InvalidOperationException("MimeMessage is not multipart/signed, it is : " + contentType);
                }

            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to retrieve content type from MimeMessage. " + e.Message, e);
            }
        }


        void parseSignedMessage()
        {
            SmimeSignedParser smimeSignedParser;
            try
            {
                // MimeMessageHelper.dumpMimePartToFile("/tmp/parseSignedMessage.txt", mimeMessage);
                // CmsTypedStream s = new CmsTypedStream(mimeMessage.GetBuffer().ToStream());
                byte[] data;
                using (var m = new MemoryStream())
                {
                    this.mimeMessage.WriteTo(m);
                    data = m.ToArray();
                }

                var messageContent = data;
                smimeSignedParser = new SmimeSignedParser(messageContent);
                    //new JcaDigestCalculatorProviderBuilder().build(),
                    //(MimeMultipart)mimeMessage.getContent());
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to create SMIMESignedParser: " + e.Message, e);
            }

            IX509Store certs;
            try
            {
                certs = smimeSignedParser.GetCertificates("Collection");
            }
            catch (CmsException e)
            {
                throw new InvalidOperationException("Unable to retrieve the certificates from signed message.");
            }

            //
            // SignerInfo blocks which contain the signatures
            //
            SignerInformationStore signerInfos;
            try
            {
                signerInfos = smimeSignedParser.GetSignerInfos();
            }
            catch (CmsException e)
            {
                throw new InvalidOperationException("Unable to get the Signer information from message. " + e.Message, e);
            }

            ICollection signers = signerInfos.GetSigners();
            IEnumerator signersIterator = signers.GetEnumerator();

            //
            // Only a single signer, get the first and only certificate
            //
            if (signersIterator.MoveNext())
            {

                // Retrieves information on first and only signer
                SignerInformation signer = (SignerInformation)signersIterator.Current;

                // Retrieves the collection of certificates for first and only signer
                // @SuppressWarnings("unchecked")
                X509CertStoreSelector sel = new X509CertStoreSelector();
                sel.Issuer = signer.SignerID.Issuer;
                sel.SerialNumber = signer.SignerID.SerialNumber;

                ICollection certCollection = certs.GetMatches(sel);

                // Retrieve the first certificate
                IEnumerator certIt = certCollection.GetEnumerator();
                if (certIt.MoveNext())
                {
                    try
                    {
                        signersX509Certificate = (X509Certificate) certIt.Current;

                        //new JcaX509CertificateConverter()
                        //    .setProvider(BouncyCastleProvider.PROVIDER_NAME)
                        //    .getCertificate((X509CertificateHolder)certIt.next());
                    }
                    catch (CertificateException e)
                    {
                        throw new InvalidOperationException("Unable to fetch certificate for signer. " + e.Message, e);
                    }
                }
                else
                {
                    throw new InvalidOperationException(
                        "Signers certificate was not found, unable to verify the signature");
                }

                // Verify that the signature is correct and that signersIterator was generated when the certificate was current
                /*
                try {
                    if (!signer.verify(new JcaSimpleSignerInfoVerifierBuilder().setProvider(BouncyCastleProvider.PROVIDER_NAME).build(signersX509Certificate))) {
                        throw new IllegalStateException("Verification of signer failed");
                    }
                } catch (CMSException | OperatorCreationException e) {
                    throw new IllegalStateException("Unable to verify the signer. " + e.getMessage(), e);
                }
                */

                String issuerDN = signersX509Certificate.IssuerDN.ToString();
                log.Debug("Certificate issued by: " + issuerDN);
            }
            else
            {
                throw new InvalidOperationException("There is no signer information available");
            }

        }
    }
}
