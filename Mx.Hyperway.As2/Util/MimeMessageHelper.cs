namespace Mx.Hyperway.As2.Util
{
    using System;
    using System.IO;
    using System.Linq;

    using log4net;

    using Microsoft.AspNetCore.Http;

    using MimeKit;

    using Mx.Hyperway.Commons.BouncyCastle;
    using Mx.Peppol.Common.Model;
    using Mx.Tools;

    public class MimeMessageHelper
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(MimeMessageHelper));

        /// <summary>
        /// Creates a MIME message from the supplied stream, which <em>must</em> contain headers,
        /// especially the header "Content-Type:"
        /// </summary>
        /// <param name="inputStream">mime stream</param>
        /// <returns></returns>
        public static MimeMessage CreateMimeMessage(Stream inputStream)
        {
            try
            {
                var msg = MimeMessage.Load(inputStream);
                return msg;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Unable to create MimeMessage from input stream. " + e.Message, e);
            }
        }

        /// <summary>
        /// Creates a MimeMultipart MIME message from an input stream, which does not contain the header "Content-Type:".
        /// Thus the mime type must be supplied as an argument.
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public static Multipart ParseMultipart(Stream contents, string mimeType)
        {
            try
            {
                var ct = ContentType.Parse(mimeType);
                var message = MimeEntity.Load(ct, contents);
                return message as Multipart;
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

        /// <summary>
        /// Creates a MIME message from the supplied InputStream, using values from the HTTP headers to
        /// do a successful MIME decoding.  If MimeType can not be extracted from the HTTP headers we
        /// still try to do a successful decoding using the payload directly.
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static MimeMessage CreateMimeMessageAssistedByHeaders(Stream inputStream, IHeaderDictionary headers)
            // throws MessagingException
        {
            string mimeType = null;
            string contentType = headers
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

            Multipart multipart;
            if (mimeType == null)
            {
                Log.Warn("Headers did not contain MIME content type, trying to decode content type from body.");
                multipart = ParseMultipart(inputStream);
            }
            else
            {
                multipart = ParseMultipart(inputStream, mimeType);
            }
            
            var mimeMessage = new MimeMessage();
            mimeMessage.Headers.Clear();
            foreach (var header in headers)
            {
                mimeMessage.Headers.Add(header.Key, header.Value);
            }

            mimeMessage.Body = multipart;
            
            return mimeMessage;
        }


        public static Multipart ParseMultipart(Stream inputStream)
        {
            // TODO: default mime properties missing
            throw new NotSupportedException("default mime properties missing");
        }

        public static Multipart ParseMultipart(string contents)
        {
            var stream = contents.ToUtf8().ToStream();
            return ParseMultipart(stream);
        }

        public static MimeMessage MultipartMimeMessage(byte[] dataSource) // throws MessagingException
        {

            Multipart mimeMultipart = MimeEntity.Load(ParserOptions.Default, dataSource.ToStream(), false) as Multipart;
            MimeMessage m = new MimeMessage();
            m.Body = mimeMultipart;
            return m;
        }

        public static MimePart CreateMimeBodyPart(Stream inputStream, string mimeType)
        {
            MimePart bodyPart;
            try
            {
                bodyPart = new MimePart(mimeType);
                bodyPart.Content = new MimeContent(inputStream);
            }
            catch (IOException e)
            {
                throw new ArgumentException("Unable to create ByteArrayDataSource from inputStream." + e.Message, e);
            }

            return bodyPart;
        }

        /// <summary>
        /// Calculates sha1 mic based on the MIME body part. 
        /// </summary>
        /// <param name="bodyPart"></param>
        /// <param name="digestMethod"></param>
        /// <returns></returns>
        public static Digest CalculateMic(MimeEntity bodyPart, SMimeDigestMethod digestMethod)
        {
            try
            {
                byte[] digest;
                using (var m = new MemoryStream())
                {
                    bodyPart.WriteTo(m);
                    m.Seek(0, SeekOrigin.Begin);
                    digest = BcHelper.Hash(m.ToArray(), digestMethod.GetAlgorithm());
                }

                return Digest.of(digestMethod.GetDigestMethod(), digest);
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

        public static string ToString(MimeMessage mimeMessage)
        {
            byte[] bytes = ToBytes(mimeMessage);
            return bytes.ToUtf8String();
        }

        public static byte[] ToBytes(MimeMessage mimeMessage)
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
