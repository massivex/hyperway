namespace Mx.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string value, string other)
        {
            return value.Equals(other, StringComparison.InvariantCultureIgnoreCase);
        }

        public static byte[] ToUtf8(this string value)
        {
            return System.Text.Encoding.UTF8.GetBytes(value);
        }

        public static String ToStringValues<T>(this T[] items)
        {
            if (items == null)
            {
                return string.Empty;
            }

            var values = items.Select(x => x.ToString()).ToArray();
            return string.Join(", ", values);
        }
        public static String ToStringValues<T>(this IEnumerable<T> items)
        {
            if (items == null)
            {
                return string.Empty;
            }

            var values = items.Select(x => x.ToString()).ToArray();
            return string.Join(", ", values);
        }

        public static string ReplaceAll(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }
    }
}
