using System;
using System.Collections.Generic;
using System.Linq;

namespace common.util {
    public static class CollectionUtil {
        public static T[] CloneArray<T>(this T[] array) where T : ICloneable {
            return Array.ConvertAll(array, e => (T)e.Clone());
        }

        private static readonly Random Random = new();

        public static IOrderedEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
            return source.OrderBy(e => Random.Next());
        }
    }
}