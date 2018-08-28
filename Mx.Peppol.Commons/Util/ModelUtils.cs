using System;
using System.Text;

namespace Mx.Peppol.Common.Util
{
    using System.Diagnostics;
    using System.Web;

    public static class ModelUtils
    {
        public static string Urlencode(string format, params object[] args)
        {
            try
            {
                Debug.Assert(args != null, nameof(args) + " != null");
                return HttpUtility.UrlEncode(string.Format(format, args), Encoding.UTF8);
            }
            catch (InvalidOperationException e)
            {
                throw new NotSupportedException("UTF-8 not supported.", e);
            }
        }

        public static string Urldecode(string value)
        {
            try
            {                
                return HttpUtility.UrlDecode(value, Encoding.UTF8);
            }
            catch (InvalidOperationException e) {
                throw new NotSupportedException("UTF-8 not supported.", e);
            }
        }
    }

}
