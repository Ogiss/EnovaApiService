using System;
using System.Collections.Generic;

namespace EnovaApiService.Framework.Helpers
{
    public static class IEnumerableExtensions
    {
        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(var item in enumerable)
            {
                action(item);
            }
        }
    }
}
