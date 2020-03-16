using System;
using System.Collections.Generic;

namespace MyWhereProject
{
    public static class MyWhereFunction
    {
        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T t in source)
            {
                if (predicate(t))
                {
                    yield return t;
                }
            }
        }
    }
}
