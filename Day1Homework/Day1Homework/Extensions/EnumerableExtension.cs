using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1Homework.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<int> GetSum<TSource>(this IEnumerable<TSource> source, int pageSize, Func<TSource, int> selector)
        {
            var index = 0;
            var enumerable = source as TSource[] ?? source.ToArray();
            while (index <= enumerable.Length)
            {
                yield return enumerable.Skip(index).Take(pageSize).Sum(selector);
                index += pageSize;
            }
        }
    }
}
