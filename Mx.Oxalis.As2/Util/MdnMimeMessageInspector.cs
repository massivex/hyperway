using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using log4net;

using MimeKit;

using Mx.Oxalis.As2.Model;
using Mx.Tools;
using Mx.Tools.Encoding;

/**
 * Inspects the various properties and parts of an MDN wrapped in a S/MIME message.
 * <p>
 * This class is typically used by the sender of the business message, which will receive
 * an MDN from the receiving party.
 * <p>
 * Part 0 : multipart/report; report-type=disposition-notification;
 * 0 : Sub part 0 : text/plain
 * 0 : Sub part 1 : message/disposition-notification
 * 0 : Sub part x : will not be used by Oxalis
 * Part 1 : application/pkcs7-signature; name=smime.p7s; smime-type=signed-data
 */
public class MdnMimeMessageInspector
{

    public static readonly ILog log = LogManager.GetLogger(typeof(MdnMimeMessageInspector));

    private readonly MimeMessage mdnMimeMessage;

    public MdnMimeMessageInspector(MimeMessage mdnMimeMessage)
    {
        this.mdnMimeMessage = mdnMimeMessage;
    }

    public List<MimeEntity> getSignedMultiPart()
    {
        try
        {
            // return (MimeMultipart)mdnMimeMessage.getContent();
            return this.mdnMimeMessage.BodyParts.ToList();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to access the contents of the MDN S/MIME message: " + e.Message, e);
        }
    }

    /**
     * The multipart/report should contain both a text/plain part with textual information and
     * a message/disposition-notification part that should be examined for error/failure/warning.
     */
    public List<MimeEntity> getMultipartReport()
    {
        try
        {
            MimeEntity bodyPart = getSignedMultiPart()[0];
            List<MimeEntity> multipartReport = (bodyPart as Multipart).ToList();
            if (!containsIgnoreCase(bodyPart.Headers[HeaderId.ContentType], "multipart/report"))
            {
                throw new InvalidOperationException("The first body part of the first part of the signed message is not a multipart/report");
            }
            return multipartReport;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to retrieve the multipart/report : " + e.Message, e);
        }
    }

    /**
     * We assume that the first text/plain part is the one containing any textual information.
     */
    public MimeEntity getPlainTextBodyPart()
    {
        return getPartFromMultipartReport("text/plain");
    }

    /**
     * We search for the first message/disposition-notification part.
     * If we don't find one of that type we assume that part 2 is the right one.
     */
    public MimeEntity getMessageDispositionNotificationPart()
    {
        MimeEntity bp = getPartFromMultipartReport("message/disposition-notification");
        if (bp == null) bp = getBodyPartAt(1); // the second part should be machine readable
        return bp;
    }

    /**
     * Return the fist part which have the given contentType
     *
     * @param contentType the mime type to look for
     */
    private MimeEntity getPartFromMultipartReport(String contentType)
    {
        try
        {
            List<MimeEntity> multipartReport = getMultipartReport();
            for (int t = 0; t < multipartReport.Count; t++)
            {
                MimeEntity bp = multipartReport[t]; //.getBodyPart(t);
                if (containsIgnoreCase(bp.Headers[HeaderId.ContentType], contentType))
                {
                    return bp;
                }
            }
        }
        catch (Exception e)
        {
            log.Error("Failed to locate part of multipart/report of type " + contentType);
        }
        return null;
    }

    /**
     * Get a specific part of the multipart/report
     *
     * @param position starts at 0 for the first, 1 for the second, etc
     */
    private MimeEntity getBodyPartAt(int position)
    {
        return getMultipartReport()[position];
    }

    public String getPlainTextPartAsText()
    {

        return getPlainTextBodyPart().ToString();
    }

    public Dictionary<String, String> getMdnFields()
    {
        Dictionary<string, string> ret = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        try
        {

            MimeEntity bp = getMessageDispositionNotificationPart();
            bool contentIsBase64Encoded = false;

            //
            // look for base64 transfer encoded MDN's (when Content-Transfer-Encoding is present)
            //
            // Content-Type: message/disposition-notification
            // Content-Transfer-Encoding: base64
            //
            // "Content-Transfer-Encoding not used in HTTP transport Because HTTP, unlike SMTP,
            // does not have an early history involving 7-bit restriction.
            // There is no need to use the Content Transfer Encodings of MIME."
            //
            String[] contentTransferEncodings = bp
                .Headers[HeaderId.ContentTransferEncoding]
                .Split(new[] { "\r\n" }, StringSplitOptions.None);
            // GetAllFieldValue("Content-Transfer-Encoding");
            if (contentTransferEncodings != null && contentTransferEncodings.Length > 0)
            {
                if (contentTransferEncodings.Length > 1)
                {
                    log.Warn("MDN has multiple Content-Transfer-Encoding, we only try the first one");
                }

                String encoding = contentTransferEncodings[0];
                if (encoding == null)
                {
                    encoding = "";
                }

                encoding = encoding.Trim();
                log.Debug("MDN specifies Content-Transfer-Encoding : '" + encoding + "'");
                if ("base64".EqualsIgnoreCase(encoding))
                {
                    contentIsBase64Encoded = true;
                }
            }

            byte[] content;
            using (var m = new MemoryStream())
            {
                bp.WriteTo(m, true);
                content = m.ToArray();
            }
            if (content != null)
            {
                // InputStream contentInputStream = (InputStream)content;

                if (contentIsBase64Encoded)
                {
                    log.Debug("MDN seems to be base64 encoded, wrapping content stream in Base64 decoding stream");
                    var base64 = new Base64Encoding();
                    content = base64.FromBytes(content);
                    // contentInputStream = (new Base64Encoding)  new Base64InputStream(contentInputStream); // wrap in base64 decoding stream
                }

                // BufferedReader r = new BufferedReader(new InputStreamReader(contentInputStream));
                using (var r = new StreamReader(content.ToStream()))

                    while (!r.EndOfStream)
                    {
                        String line = r.ReadLine();
                        int firstColon = line.IndexOf(":"); // "Disposition: ......"
                        if (firstColon > 0)
                        {
                            String key = line.Substring(0, firstColon).Trim(); // up to :
                            String value = line.Substring(firstColon + 1).Trim(); // skip :
                            ret.Add(key, value);
                        }
                    }

            }
            else
            {
                throw new Exception("Unsupported MDN content, expected InputStream found @ " + content.ToString());
            }
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to retrieve the values from the MDN : " + e.Message, e);
        }

        return ret;
    }

    /**
     * Decode MDN and make sure the message was processed (allow for warnings)
     *
     * @param outboundMic the outbound mic to verify against
     */
    public bool isOkOrWarning(Mic outboundMic)
    {

        Dictionary<String, String> mdnFields = getMdnFields();

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
        String disposition = mdnFields["Disposition"];
        if (disposition == null)
        {
            log.Error("Unable to retreieve 'Disposition' from MDN");
            return false;
        }

        log.DebugFormat("Decoding received disposition ({0})", disposition);
        As2Disposition as2dis = As2Disposition.valueOf(disposition);

        // make sure we are in processed state
        if (!As2Disposition.DispositionType.PROCESSED.Equals(as2dis.getDispositionType()))
        {
            // Disposition: automatic-action/MDN-sent-automatically; failed/failure: sender-equals-receiver
            log.Error("Failed or unknown state : " + disposition);
            return false;
        }

        // check if the returned MIC matches our outgoing MIC (sha1 of payload), warn about mic mismatch
        String receivedMic = mdnFields["Received-Content-MIC"];
        if (receivedMic == null)
        {
            log.Error("MIC error, no Received-Content-MIC returned in MDN");
            return false;
        }
        if (!outboundMic.Equals(Mic.valueOf(receivedMic)))
        {
            log.Warn("MIC mismatch, Received-Content-MIC was : " + receivedMic + " while Outgoing-MIC was : " + outboundMic.ToString());
            return false;
        }

        // return when "clean processing state" : Disposition: automatic-action/MDN-sent-automatically; processed
        As2Disposition.DispositionModifier modifier = as2dis.getDispositionModifier();
        if (modifier == null) return true;

        // allow partial success (warning)
        if (As2Disposition.DispositionModifier.Prefix.WARNING.Equals(modifier.getPrefix()))
        {
            // Disposition: automatic-action/MDN-sent-automatically; processed/warning: duplicate-document
            log.Warn("Returns with warning : " + disposition);
            return true;
        }

        // Disposition: automatic-action/MDN-sent-automatically; processed/error: insufficient-message-security
        log.Warn("MDN failed with disposition raw : " + disposition);
        log.Warn("MDN failed with as2 disposition : " + as2dis.ToString());

        return false;
    }

    /**
     * Returns true if and only if the first param string contains the specified
     * string of second parameter ignoring case.
     *
     * @param containerString the sequence to search for
     * @param s               the sequence to search for
     * @return true if this string contains {@code s}, false otherwise
     */
    private static bool containsIgnoreCase(String containerString, String s)
    {
        if (containerString == null || s == null)
        {
            return false;
        }
        return containerString.ToLowerInvariant().Contains(s.ToLowerInvariant());
    }

}
