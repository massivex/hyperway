using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Util
{
    using System.ComponentModel;
    using System.IO;
    using System.Linq;

    using log4net;

    using Mx.Mime;
    using Mx.Oxalis.Commons.BouncyCastle;
    using Mx.Peppol.Common.Model;
    using Mx.Tools;
    using Mx.Tools.Encoding;

    using Org.BouncyCastle.Security;

    public class MimeMessageHelper
    {

        private static Base64Encoding encoder = new Base64Encoding();

        public static readonly ILog log = LogManager.GetLogger(typeof(MimeMessageHelper));

        /**
         * Creates a MIME message from the supplied stream, which <em>must</em> contain headers,
         * especially the header "Content-Type:"
         */
        public static MimeMessage createMimeMessage(Stream inputStream)
        {
            try
            {
                //Properties properties = System.getProperties();
                //Session session = Session.getDefaultInstance(properties, null);
                var msg = new MimeMessage();
                var data = Encoding.UTF8.GetString(inputStream.ToBuffer());
                msg.LoadBody(data);
                return msg;
                // return new MimeMessage(session, inputStream);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Unable to create MimeMessage from input stream. " + e.Message, e);
            }
        }

        /**
         * Creates a MimeMultipart MIME message from an input stream, which does not contain the header "Content-Type:".
         * Thus the mime type must be supplied as an argument.
         */
        public static MimeMessage parseMultipart(Stream contents, String mimeType)
        {
            try
            {
                // TODO: ByteArrayDataSource with mimeType not implmemented
                throw new NotImplementedException("unable to " + mimeType);
                // ByteArrayDataSource dataSource = new ByteArrayDataSource(contents, mimeType);
                // return multipartMimeMessage(dataSource);
            }
            catch (IOException e)
            {
                throw new ArgumentException("Unable to create ByteArrayDataSource; " + e.Message, e);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Unable to create Multipart mime message; " + e.Message, e);
            }
        }

        /**
         * Creates a MIME message from the supplied InputStream, using values from the HTTP headers to
         * do a successful MIME decoding.  If MimeType can not be extracted from the HTTP headers we
         * still try to do a successful decoding using the payload directly.
         */
        public static MimeMessage createMimeMessageAssistedByHeaders(Stream inputStream, MimeField[] headers)
            // throws MessagingException
        {
            String mimeType = null;
            String contentType = headers
                .Where(x => x.GetName() == MimeConst.ContentType)
                .Select(x => x.GetValue())
                .FirstOrDefault();
            if (contentType != null)
            {
                // From rfc2616 :
                // Multiple message-header fields with the same field-name MAY be present in a message if and only
                // if the entire field-value for that header field is defined as a comma-separated list.
                // It MUST be possible to combine the multiple header fields into one "field-name: field-value" pair,
                // without changing the semantics of the message, by appending each subsequent field-value to the first,
                // each separated by a comma.
                mimeType = contentType;
            }

            MimeMessage mimeMessage;
            if (mimeType == null)
            {
                log.Warn("Headers did not contain MIME content type, trying to decode content type from body.");
                mimeMessage = MimeMessageHelper.parseMultipart(inputStream);
            }
            else
            {
                mimeMessage = MimeMessageHelper.parseMultipart(inputStream, mimeType);
            }
            
            
            foreach (MimeField header in headers)
            {
                mimeMessage.SetFieldValue(header.GetName(), header.GetValue(), header.GetCharset());
                // mimeMessage.addHeader(header.getName(), header.getValue());
            }

            return mimeMessage;
        }


        public static MimeMessage parseMultipart(Stream inputStream)
        {
            try
            {
                // TODO: default mime properties missing
                throw new NotSupportedException("default mime properties missing");
                // return new MimeMessage(Session.getDefaultInstance(System.getProperties()), inputStream);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static MimeMessage parseMultipart(String contents)
        {
            var stream = contents.ToUtf8().ToStream();
            // ByteArrayInputStream byteArrayInputStream = new ByteArrayInputStream(contents.getBytes());
            return parseMultipart(stream);
        }

        public static MimeMessage multipartMimeMessage(byte[] dataSource) // throws MessagingException
        {

            // MimeBody mimeMultipart = new MimeBody();
            // mimeMultipart.LoadBody(dataSource, Encoding.UTF8);
            // MimeMessage mimeMessage = new MimeMessage(Session.getDefaultInstance(System.getProperties()));
            // mimeMultipart
            //mimeMessage.setContent(mimeMultipart);
            MimeMessage m = new MimeMessage();
            m.LoadBody(dataSource, Encoding.UTF8);
            return m;
        }

        public static MimeMessage createMimeBodyPart(Stream inputStream, String mimeType)
        {
            var mimeBodyPart = new MimeMessage();
            
            // ByteArrayDataSource byteArrayDataSource;

            try
            {
                mimeBodyPart.LoadBody(inputStream.ToBuffer().ToUtf8String());
                // byteArrayDataSource = new ByteArrayDataSource(inputStream, mimeType);
            }
            catch (IOException e)
            {
                throw new ArgumentException("Unable to create ByteArrayDataSource from inputStream." + e.Message, e);
            }

            // TODO: Check what's DataHandler for mimeBodyPart
            //mimeBodyPart.
            //try
            //{
            //    DataHandler dh = new DataHandler(byteArrayDataSource);
            //    mimeBodyPart.setDataHandler(dh);
            //}
            //catch (MessagingException e)
            //{
            //    throw new IllegalStateException("Unable to set data handler on mime body part." + e.getMessage(), e);
            //}

            try
            {
                mimeBodyPart.SetFieldValue(MimeConst.ContentType, mimeType, null); // .setHeader("Content-Type", mimeType);
                mimeBodyPart.SetFieldValue(MimeConst.TransferEncoding, "binary", null); // .setHeader("Content-Transfer-Encoding", "binary");
                                                                                  //  No content-transfer-encoding needed for http
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to set headers." + e.Message, e);
            }

            return mimeBodyPart;
        }

        /**
         * Calculates sha1 mic based on the MIME body part.
         */
        public static Digest calculateMic(MimeBody bodyPart, SMimeDigestMethod digestMethod)
        {
            try
            {
                
                // MessageDigest md = BCHelper.getMessageDigest(digestMethod.getIdentifier());
                var digest = BcHelper.Hash(bodyPart.GetBuffer(), digestMethod.getAlgorithm());
                // bodyPart.writeTo(new DigestOutputStream(ByteStreams.nullOutputStream(), md));
                return Digest.of(digestMethod.getDigestMethod(), digest);
            }
            catch (IOException e)
            {
                throw new InvalidOperationException("Unable to read data from digest input. " + e.Message, e);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to handle mime body part. " + e.Message, e);
            }
        }

        public static String toString(MimeMessage mimeMessage)
        {
            byte[] bytes = toBytes(mimeMessage);
            return bytes.ToUtf8String();
        }

        public static byte[] toBytes(MimeMessage mimeMessage)
        {
            byte[] result;
            using (var buffer = new MemoryStream())
            {
                try

                {
                    mimeMessage.WriteTo(buffer);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Unable to convert MDN mime message into bytes()", e);
                }
                result = buffer.ToBuffer();
            }

            return result;
        }
    }
}
