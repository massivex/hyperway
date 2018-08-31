using System;
using System.Collections.Generic;

namespace Mx.Tools
{
    public static class ArrayExtensions
    {
        public static IEnumerable<TItem> Peek<TItem>(this IEnumerable<TItem> list, Action<TItem> action)
        {
            foreach (var item in list)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<TOutput> Map<TInput, TOutput>(this IEnumerable<TInput> list, Func<TInput, TOutput> action)
        {
            foreach (var item in list)
            {
                yield return action(item);
            }
        }
    }
}
