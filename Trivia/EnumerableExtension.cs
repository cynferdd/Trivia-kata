using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source) => 
            source.SelectMany(c => c);

        public static CircularIterator<T> ToCircular<T>(this IEnumerable<T> source) =>
            new CircularIterator<T>(source.ToList());
    }
}
