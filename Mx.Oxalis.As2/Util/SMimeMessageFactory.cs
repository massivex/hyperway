using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Util
{
    using System.Collections;
    using System.IO;

    using Mx.Mime;
    using Mx.Oxalis.Api.Lang;
    using Mx.Peppol.Common.Model;
    using Mx.Tools;

    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.Cms;
    using Org.BouncyCastle.Cms;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Operators;
    using Org.BouncyCastle.Security.Certificates;
    using Org.BouncyCastle.X509;
    using Org.BouncyCastle.X509.Store;

    public class SMimeMessageFactory
    {

        private readonly AsymmetricKeyParameter privateKey;

        private readonly X509Certificate ourCertificate;

        //   private readonly static Session session = Session.getDefaultInstance(System.getProperties(), null);

        //static {
        //    BCHelper.registerProvider();
        //}

        //        @Inject

        public SMimeMessageFactory(AsymmetricKeyParameter privateKey, X509Certificate ourCertificate)
        {
            this.privateKey = privateKey;
            this.ourCertificate = ourCertificate;
        }

        /**
         * Creates an S/MIME message from the supplied String, having the supplied MimeType as the "content-type".
         *
         * @param msg      holds the payload of the message
         * @param mimeType the MIME type to be used as the "Content-Type"
         */
        public MimeMessage
            createSignedMimeMessage(
                string msg,
                MimeType mimeType,
                SMimeDigestMethod digestMethod) // throws OxalisTransmissionException
        {
            var msgData = Encoding.Default.GetBytes(msg);
            return createSignedMimeMessage(msgData.ToStream(), mimeType, digestMethod);
        }

        /**
         * Creates a new S/MIME message having the supplied MimeType as the "content-type"
         */
        public MimeMessage createSignedMimeMessage(
            Stream inputStream,
            MimeType mimeType,
            SMimeDigestMethod digestMethod) // throws OxalisTransmissionException
        {
            MimeMessage mimeBodyPart = MimeMessageHelper.createMimeBodyPart(inputStream, mimeType.ToString());
            return createSignedMimeMessage(mimeBodyPart, digestMethod);
        }

        /**
         * Creates an S/MIME message using the supplied MimeBodyPart. The signature is generated using the private key
         * as supplied in the constructor. Our certificate, which is required to verify the signature is enclosed.
         */
        public MimeMessage createSignedMimeMessage(MimeMessage mimeBodyPart, SMimeDigestMethod digestMethod)
            // throws OxalisTransmissionException
        {

            //
            // S/MIME capabilities are required, but we simply supply an empty vector
            //
            Asn1EncodableVector signedAttrs = new Asn1EncodableVector();

            //
            // create the generator for creating an smime/signed message
            //
            CmsSignedDataGenerator
                smimeSignedGenerator =
                    new CmsSignedDataGenerator(); // SMIMESignedGenerator("binary"); //also see CMSSignedGenerator ?

            //
            // add a signer to the generator - this specifies we are using SHA1 and
            // adding the smime attributes above to the signed attributes that
            // will be generated as part of the signature. The encryption algorithm
            // used is taken from the key - in this RSA with PKCS1Padding
            //
            CmsAttributeTableGenerator cmsAttrGenerator = new SimpleAttributeTableGenerator(new AttributeTable(signedAttrs));
            Asn1SignatureFactory signatureFactory = new Asn1SignatureFactory(digestMethod.getMethod(), this.privateKey);
            try

            {
                // .setSignedAttributeGenerator(new AttributeTable(signedAttrs))
                // .setProvider(BouncyCastleProvider.PROVIDER_NAME)
                //  digestMethod.getMethod(), privateKey, ourCertificate));
                // .build("SHA1withRSA", privateKey, ourCertificate));
                var signer = new SignerInfoGeneratorBuilder().WithSignedAttributeGenerator(cmsAttrGenerator)
                    .Build(signatureFactory, this.ourCertificate);

                smimeSignedGenerator.AddSignerInfoGenerator(signer);
            }
            catch (CertificateEncodingException e)
            {
                throw new OxalisTransmissionException($"Certificate encoding problems while adding signer information. {e.Message}", e);
            }
            catch (Exception e)
            {
                throw new OxalisTransmissionException("Unable to add Signer information. " + e.Message, e);
            }

            //
            // create a CertStore containing the certificates we want carried
            // in the signature
            //
            IX509Store certs;
            try
            {
                certs = X509StoreFactory.Create(
                    "Certificate/Collection",
                    new X509CollectionStoreParameters(new ArrayList() { this.ourCertificate }));
            }
            catch (CertificateEncodingException e)
            {
                throw new OxalisTransmissionException(
                    "Unable to create CertStore with our certificate. " + e.Message,
                    e);
            }
            smimeSignedGenerator.AddCertificates(certs);

            //
            // Signs the supplied MimeBodyPart
            //
            var data = mimeBodyPart.GetBuffer();
            CmsProcessableByteArray cmsContent = new CmsProcessableByteArray(data);
            CmsSignedData mimeMultipart;
            try
            {
                mimeMultipart = smimeSignedGenerator.Generate(cmsContent);
            }
            catch (Exception e)
            {
                throw new OxalisTransmissionException("Unable to generate signed mime multipart." + e.Message, e);
            }

            //
            // Get a Session object and create the mail message
            //
            // Properties props = System.getProperties();
            // Session session = Session.getDefaultInstance(props, null);

            MimeMessage mimeMessage = new MimeMessage();
            
            
            try
            {
                // mimeMessage.LoadBody(mimeMultipart); // v.getContentType());
                mimeMessage.LoadBody(mimeMultipart.GetEncoded(), Encoding.ASCII);
            }
            catch (Exception e)
            {
                throw new OxalisTransmissionException($"Unable to  set Content type of MimeMessage. {e.Message}", e);
            }

            //try
            //{
            //    mimeMessage.saveChanges();
            //}
            //catch (MessagingException e)
            //{
            //    throw new OxalisTransmissionException("Unable to save changes to Mime message. " + e.getMessage(), e);
            //}

            return mimeMessage;
        }

        //public MimeMessage createSignedMimeMessageNew(
        //        MimeMessage mimeBodyPart,
        //        Digest digest,
        //        SMimeDigestMethod digestMethod)
        //    // throws OxalisTransmissionException
        //{
        //    try
        //    {
        //        MimeMultipart mimeMultipart = new MimeMultipart();
        //        mimeMultipart.setSubType("signed");
        //        mimeMultipart.addBodyPart(mimeBodyPart);

        //        MimeBodyPart signaturePart = new MimeBodyPart();
        //        DataSource dataSource = new ByteArrayDataSource(
        //            SMimeBC.createSignature(digest.getValue(), digestMethod, privateKey, ourCertificate),
        //            "application/pkcs7-signature");
        //        signaturePart.setDataHandler(new DataHandler(dataSource));
        //        signaturePart.setHeader(
        //            "Content-Type",
        //            "application/pkcs7-signature; name=smime.p7s; smime-type=signed-data");
        //        signaturePart.setHeader("Content-Transfer-Encoding", "base64");
        //        signaturePart.setHeader("Content-Disposition", "attachment; filename=\"smime.p7s\"");
        //        signaturePart.setHeader("Content-Description", "S/MIME Cryptographic Signature");
        //        mimeMultipart.addBodyPart(signaturePart);

        //        MimeMessage mimeMessage = new MimeMessage(session);
        //        mimeMessage.setContent(mimeMultipart, mimeMultipart.getContentType());
        //        mimeMessage.saveChanges();

        //        return mimeMessage;
        //    }
        //    catch (OxalisSecurityException e)
        //    {
        //        throw new OxalisTransmissionException(e.Message, e);
        //    }
        //}
    }
}