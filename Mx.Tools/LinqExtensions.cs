using System;
using System.Collections.Generic;

namespace Mx.Tools
{
    using System.Linq;

    public static class LinqExtensions
    {
        public static IEnumerable<object> OfTypes<T>(this IEnumerable<T> list, params Type[] types)
        {
            var typeNames = types.Select(x => x.FullName).ToArray();
            return list
                    .Where(x => x != null)
                    .Where(x => typeNames.Contains(x.GetType().FullName))
                    .OfType<object>();
        }
    }
}
