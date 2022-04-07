using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace generator.util {
    public static class CollectionUtil {
        public static List<T> Clone<T>(this Collection<T> collection) where T : ICloneable {
            var list = new List<T>(collection.Count);
            list.AddRange(collection);
            return list;
        }

        public static T[] CloneArray<T>(this T[] array) where T : ICloneable {
            return Array.ConvertAll(array, e => (T)e.Clone());
        }
    }
}