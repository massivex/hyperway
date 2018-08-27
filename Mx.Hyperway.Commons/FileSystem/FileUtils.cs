namespace Mx.Hyperway.Commons.FileSystem
{
    using System;
    using System.Text.RegularExpressions;

    public class FileUtils
    {
        /// <summary>
        /// Filter string to make it better fit use as filename. 
        /// </summary>
        /// <param name="s">Unfiltered string.</param>
        /// <returns>Filtered string.</returns>
        public static String FilterString(String s)
        {
            return Regex.Replace(s, "[^a-zA-Z0-9.\\-]", "_");
        }
    }
}
