using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Util
{
    using System.Diagnostics;
    using System.Linq;
    using System.Web;

    public static class ModelUtils
    {
        public static String urlencode(String format, params string[] args)
        {
            try
            {
                Debug.Assert(args != null, nameof(args) + " != null");
                return HttpUtility.UrlEncode(String.Format(format, args), Encoding.UTF8);
            }
            catch (InvalidOperationException e)
            {
                throw new NotSupportedException("UTF-8 not supported.", e);
            }
        }

        public static String urldecode(String value)
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
