using System;
using System.Text;

namespace Mx.Oxalis.As2.Util
{
    using System.IO;
    using System.Linq;

    using log4net;

    using Microsoft.AspNetCore.Http;

    using MimeKit;

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
                var msg = MimeMessage.Load(inputStream);
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
        public static MimeMessage createMimeMessageAssistedByHeaders(Stream inputStream, IHeaderDictionary headers)
            // throws MessagingException
        {
            String mimeType = null;
            String contentType = headers
                .Where(x => x.Key == HeaderId.ContentType.ToHeaderName())
                .Select(x => x.Value)
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
            
            
            foreach (var header in headers)
            {
                mimeMessage.Headers.Add(header.Key, header.Value);
                // mimeMessage.SetFieldValue(header.Field, header.Value, header.GetCharset());
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
            Multipart mimeMultipart = Multipart.Load(ParserOptions.Default, dataSource.ToStream(), false) as Multipart;
            // mimeMultipart.Prepare();
            MimeMessage m = new MimeMessage();
            // m.LoadBody(dataSource, Encoding.UTF8);
            m.Body = mimeMultipart;
            return m;
        }

        public static MimePart createMimeBodyPart(Stream inputStream, String mimeType)
        {
            // var mimeMessage = new MimeMessage();

            // ByteArrayDataSource byteArrayDataSource;
            MimePart bodyPart;
            try
            {
                bodyPart = new MimePart(mimeType);
                bodyPart.Content = new MimeContent(inputStream, ContentEncoding.Binary);
                bodyPart.ContentTransferEncoding = ContentEncoding.Binary;
                
                //var parseOption = new ParserOptions();

                //parseOption.CharsetEncoding = Encoding.UTF8;
                //inputStream.Seek(0, SeekOrigin.Begin);
                //bodyPart = MimeEntity.Load(parseOption, ContentType.Parse(mimeType), inputStream);                
                // mimeBodyPart.LoadBody(inputStream.ToBuffer().ToUtf8String());
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

            //try
            //{
            //    bodyPart.SetFieldValue(MimeConst.ContentType, mimeType, null); // .setHeader("Content-Type", mimeType);
            //    bodyPart.SetFieldValue(MimeConst.TransferEncoding, "binary", null); // .setHeader("Content-Transfer-Encoding", "binary");
            //                                                                      //  No content-transfer-encoding needed for http
            //}
            //catch (Exception e)
            //{
            //    throw new InvalidOperationException("Unable to set headers." + e.Message, e);
            //}

            return bodyPart;
        }

        /**
         * Calculates sha1 mic based on the MIME body part.
         */
        public static Digest calculateMic(MimeEntity bodyPart, SMimeDigestMethod digestMethod)
        {
            try
            {
                byte[] digest;
                using (var m = new MemoryStream())
                {
                    bodyPart.WriteTo(m);
                    m.Seek(0, SeekOrigin.Begin);
                    digest = BcHelper.Hash(m.ToArray(), digestMethod.getAlgorithm());
                    var test = BitConverter.ToString(digest).Replace("-", "");
                }

                return Digest.of(digestMethod.getDigestMethod(), digest);
                // MessageDigest md = BCHelper.getMessageDigest(digestMethod.getIdentifier());

                // bodyPart.writeTo(new DigestOutputStream(ByteStreams.nullOutputStream(), md));

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
