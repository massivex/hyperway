using System;

using log4net;

using MimeKit;

namespace Mx.Hyperway.As2.Util
{
    using MimeKit.Cryptography;

    using Mx.Hyperway.As2.Model;

    /// <summary>
    ///  Inspects the various properties and parts of an MDN wrapped in a S/MIME message.
    /// <p>
    /// This class is typically used by the sender of the business message, which will receive
    /// an MDN from the receiving party.
    /// </p>
    /// Part 0 : multipart/report; report-type=disposition-notification;
    /// 0 : Sub part 0 : text/plain
    /// 0 : Sub part 1 : message/disposition-notification
    /// 0 : Sub part x : will not be used by Hyperway
    /// Part 1 : application/pkcs7-signature; name=smime.p7s; smime-type=signed-data
    /// </summary>
    public class MdnMimeMessageInspector
    {

        public static readonly ILog Log = LogManager.GetLogger(typeof(MdnMimeMessageInspector));

        private readonly MimeMessage mdnMimeMessage;

        public MdnMimeMessageInspector(MimeMessage mdnMimeMessage)
        {
            this.mdnMimeMessage = mdnMimeMessage;
        }

        public MultipartSigned GetSignedMultiPart()
        {
            try
            {
                return (MultipartSigned)this.mdnMimeMessage.Body;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    "Unable to access the contents of the MDN S/MIME message: " + e.Message,
                    e);
            }
        }

        /// <summary>
        /// The multipart/report should contain both a text/plain part with textual information and
        /// a message/disposition-notification part that should be examined for error/failure/warning.
        /// </summary>
        /// <returns></returns>
        public MultipartReport GetMultipartReport()
        {
            try
            {
                MultipartReport bodyPart = this.GetSignedMultiPart()[0] as MultipartReport;
                if (bodyPart == null)
                {
                    throw new InvalidOperationException(
                        "The first body part of the first part of the signed message is not a multipart/report");
                }

                return bodyPart;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to retrieve the multipart/report : " + e.Message, e);
            }
        }


        /// <summary>
        /// We assume that the first text/plain part is the one containing any textual information.
        /// </summary>
        /// <returns></returns>
        public TextPart GetPlainTextBodyPart()
        {
            return (TextPart)this.GetPartFromMultipartReport("text/plain");
        }

        /// <summary>
        /// We search for the first message/disposition-notification part.
        /// If we don't find one of that type we assume that part 2 is the right one.
        /// </summary>
        /// <returns></returns>
        public MessageDispositionNotification GetMessageDispositionNotificationPart()
        {
            var bp = (MessageDispositionNotification)this.GetPartFromMultipartReport(
                "message/disposition-notification");
            return bp;
        }

        /// <summary>
        /// Return the fist part which have the given contentType
        /// </summary>
        /// <param name="contentType">the mime type to look for</param>
        /// <returns></returns>
        private MimeEntity GetPartFromMultipartReport(string contentType)
        {
            try
            {
                MultipartReport multipartReport = this.GetMultipartReport();
                for (int t = 0; t < multipartReport.Count; t++)
                {
                    MimeEntity bp = multipartReport[t];
                    if (ContainsIgnoreCase(bp.ContentType.MimeType, contentType))
                    {
                        return bp;
                    }
                }
            }
            catch (Exception)
            {
                Log.Error("Failed to locate part of multipart/report of type " + contentType);
            }

            return null;
        }

        public string GetPlainTextPartAsText()
        {

            return this.GetPlainTextBodyPart().Text;
        }

        public HeaderList GetMdnFields()
        {
            MessageDispositionNotification bp = this.GetMessageDispositionNotificationPart();
            return bp.Fields;
        }

        /// <summary>
        /// Decode MDN and make sure the message was processed (allow for warnings)
        /// </summary>
        /// <param name="outboundMic">the outbound mic to verify against</param>
        /// <returns></returns>
        public bool IsOkOrWarning(Mic outboundMic)
        {

            HeaderList mdnFields = this.GetMdnFields();

            /*
            --------_=_NextPart_001_B096DD27.9007A6CE
            Content-Type: message/disposition-notification

            Reporting-UA: AS2 eefacta Server (unimaze.com)
            Original-Recipient: rfc822; SMP_2000000005
            Final-Recipient: rfc822; SMP_2000000005
            Original-Message-ID: a60d9982-680c-4f01-9ab4-9b5d5fb05f37
            Received-Content-MIC: ZMY/AoJb2JQS557MOATtc0EZdZQ=, sha1
            Disposition: automatic-action/MDN-sent-automatically; processed


            --------_=_NextPart_001_B096DD27.9007A6CE--
            */

            // make sure we have a valid disposition
            string disposition = mdnFields["Disposition"];
            if (disposition == null)
            {
                Log.Error("Unable to retreieve 'Disposition' from MDN");
                return false;
            }

            Log.DebugFormat("Decoding received disposition ({0})", disposition);
            As2Disposition as2Dis = As2Disposition.valueOf(disposition);

            // make sure we are in processed state
            if (!DispositionType.Processed.Equals(as2Dis.DispositionType))
            {
                // Disposition: automatic-action/MDN-sent-automatically; failed/failure: sender-equals-receiver
                Log.Error("Failed or unknown state : " + disposition);
                return false;
            }

            // check if the returned MIC matches our outgoing MIC (sha1 of payload), warn about mic mismatch
            string receivedMic = mdnFields["Received-Content-MIC"];
            if (receivedMic == null)
            {
                Log.Error("MIC error, no Received-Content-MIC returned in MDN");
                return false;
            }

            if (!outboundMic.Equals(Mic.ValueOf(receivedMic)))
            {
                Log.Warn(
                    "MIC mismatch, Received-Content-MIC was : " + receivedMic + " while Outgoing-MIC was : "
                    + outboundMic);
                return false;
            }

            // return when "clean processing state" : Disposition: automatic-action/MDN-sent-automatically; processed
            DispositionModifier modifier = as2Dis.getDispositionModifier();
            if (modifier == null) return true;

            // allow partial success (warning)
            if (DispositionModifierPrefix.Warning.Equals(modifier.GetPrefix()))
            {
                // Disposition: automatic-action/MDN-sent-automatically; processed/warning: duplicate-document
                Log.Warn("Returns with warning : " + disposition);
                return true;
            }

            // Disposition: automatic-action/MDN-sent-automatically; processed/error: insufficient-message-security
            Log.Warn("MDN failed with disposition raw : " + disposition);
            Log.Warn("MDN failed with as2 disposition : " + as2Dis);

            return false;
        }

        /// <summary>
        /// Returns true if and only if the first param string contains the specified
        /// string of second parameter ignoring case.
        /// </summary>
        /// <param name="containerString">the sequence to search for</param>
        /// <param name="s">the sequence to search for</param>
        /// <returns>true if this string contains {@code s}, false otherwise</returns>
        private static bool ContainsIgnoreCase(string containerString, string s)
        {
            if (containerString == null || s == null)
            {
                return false;
            }

            return containerString.ToLowerInvariant().Contains(s.ToLowerInvariant());
        }
    }
}
