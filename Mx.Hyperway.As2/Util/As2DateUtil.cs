﻿namespace Mx.Hyperway.As2.Util
{
    using System;
    using System.Globalization;

    /*
     * Ensures that all date time objects are parsed and formatted according to the specifications in RFC4130.
     * <p>
     * RFC-4130 references RFC-2045, which references RFC-1123, which references good old RFC-822.
     * <pre>
     * date-time   =  [ day "," ] date time        ; dd mm yy
     * ;  hh:mm:ss zzz
     *
     * day         =  "Mon"  / "Tue" /  "Wed"  / "Thu"
     * /  "Fri"  / "Sat" /  "Sun"
     *
     * date        =  1*2DIGIT month 4DIGIT           ; day month year(4 digits)
     * ;  e.g. 20 Jun 82
     *
     * month       =  "Jan"  /  "Feb" /  "Mar"  /  "Apr"
     * /  "May"  /  "Jun" /  "Jul"  /  "Aug"
     * /  "Sep"  /  "Oct" /  "Nov"  /  "Dec"
     *
     * time        =  hour zone                       ; ANSI and Military
     *
     * hour        =  2DIGIT ":" 2DIGIT [":" 2DIGIT]
     * ; 00:00:00 - 23:59:59
     *
     * zone        =  "UT"  / "GMT"                   ; Universal Time
     * ; North American : UT
     * /  "EST" / "EDT"                    ;  Eastern:  - 5/ - 4
     * /  "CST" / "CDT"                    ;  Central:  - 6/ - 5
     * /  "MST" / "MDT"                    ;  Mountain: - 7/ - 6
     * /  "PST" / "PDT"                    ;  Pacific:  - 8/ - 7
     * /  1ALPHA                           ; Military: Z = UT;
     * ;  A:-1; (J not used)
     * ;  M:-12; N:+1; Y:+12
     * / ( ("+" / "-") 4DIGIT )            ; Local differential
     * ;  hours+min. (HHMM)
     * </pre>
     *
     * @see "RFC-4130"
     * @see "RFC-2045"
     * @see "RFC-1123"
     * @see "RFC-822"
     */
    /// <summary>
    /// Ensures that all date time objects are parsed and formatted according to the specifications in RFC4130.
    /// <p>
    /// RFC-4130 references RFC-2045, which references RFC-1123, which references good old RFC-822.
    /// </p>
    ///
    /// date-time   =  [day "," ] date time; dd mm yy;  hh:mm:ss zzz
    ///
    /// day = "Mon" / "Tue" / "Wed" / "Thu"
    /// / "Fri" / "Sat" / "Sun"
    ///
    /// / date = 1 * 2DIGIT month 4DIGIT           ; day month year(4 digits)
    /// ;  e.g. 20 Jun 82
    ///
    /// month = "Jan" / "Feb" / "Mar" / "Apr"
    /// / "May" / "Jun" / "Jul" / "Aug"
    /// / "Sep" / "Oct" / "Nov" / "Dec"
    ///
    /// time = hour zone                       ; ANSI and Military
    ///
    /// hour = 2DIGIT ":" 2DIGIT[":" 2DIGIT]
    /// ; 00:00:00 - 23:59:59
    ///
    /// zone = "UT" / "GMT"; Universal Time
    /// ; North American : UT
    /// /  "EST" / "EDT"                    ;  Eastern:  - 5/ - 4
    /// /  "CST" / "CDT"                    ;  Central:  - 6/ - 5
    /// /  "MST" / "MDT"                    ;  Mountain: - 7/ - 6
    /// /  "PST" / "PDT"                    ;  Pacific:  - 8/ - 7
    /// /  1ALPHA                           ; Military: Z = UT;
    /// ;  A:-1; (J not used)
    /// ;  M:-12; N:+1; Y:+12
    /// / (("+" / "-") 4DIGIT )            ; Local differential
    /// ;  hours+min. (HHMM)
    /// 
    ///
    /// <seealso>RFC-4130</seealso>
    /// <seealso>RFC-1123</seealso>
    /// <seealso>RFC-822</seealso>
    /// </summary>
    public class As2DateUtil
    {
        public static readonly As2DateUtil Rfc822 = new As2DateUtil("ddd, dd MMM yyyy HH:mm:ss zzz");

        public static readonly As2DateUtil Iso8601 = new As2DateUtil("yyyy-MM-dd'T'HH:mm:ss.SSSXXX");

        private String format;

        As2DateUtil(String format)
        {
            this.format = format;
        }

        public DateTime Parse(String s)
        {
            try
            {
                DateTime date = DateTime.ParseExact(s, this.format, CultureInfo.InvariantCulture);
                return date;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message, e);
            }
        }

        public String GetFormat(DateTime d)
        {
            var dateTxt = d.ToString(this.format, CultureInfo.InvariantCulture);
            if (dateTxt.Length == 32)
            {
                dateTxt = dateTxt.Substring(0, 29) + dateTxt.Substring(30);
            }

            return dateTxt;
        }
    }
}
