using System.Collections.Generic;

namespace NeuroDerby.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool HasDuplicates<T>(this IEnumerable<T> myList) {
            var hashSet = new HashSet<T>();

            foreach (var val in myList)
                if (!hashSet.Add(val)) return true;
            return false;
        }
    }
}