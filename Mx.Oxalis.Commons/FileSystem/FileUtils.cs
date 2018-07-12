using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Commons.FileSystem
{
    using System.IO;
    using System.Text.RegularExpressions;

    public class FileUtils
    {

        /**
         * Filter string to make it better fit use as filename.
         *
         * @param s Unfiltered string.
         * @return Filtered string.
         */
        public static String filterString(String s)
        {
            return Regex.Replace(s, "[^a-zA-Z0-9.\\-]", "_");
        }

        //public static Uri toUrl(Path path)
        //{
        //    try
        //    {
        //        return path.toUri().toURL();
        //    }
        //    catch (MalformedURLException e)
        //    {
        //        throw new IllegalStateException(e.getMessage(), e);
        //    }
        //}
    }
}
