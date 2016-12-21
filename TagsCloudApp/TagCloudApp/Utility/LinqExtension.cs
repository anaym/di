using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
    public static class LinqExtension
    {
        public static T MinOrDefault<T>(this IEnumerable<T> seq, Func<T, int> keyExtractor)
        {
            var minKey = int.MaxValue;
            var data = default(T);
            foreach (var item in seq)
            {
                var currentKey = keyExtractor(item);
                if (currentKey < minKey)
                {
                    minKey = currentKey;
                    data = item;
                }
            }
            return data;
        }

        public static Dictionary<TK, TV> ToDictionary<TK, TV>(this IEnumerable<KeyValuePair<TK, TV>> seq)
        {
            return seq.ToDictionary(p => p.Key, p => p.Value);
        }

        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> seq, Func<T, bool> validator)
        {
            return seq.Where(i => !validator(i));
        }

        public static IEnumerable<T> Any<T>(this IEnumerable<T> seq, Func<T, bool> comparer, out bool isAny)
        {
            var any = false;
            seq = seq.Parallell(i => any = comparer(i));
            isAny = any;
            return seq;
        }

        public static IEnumerable<T> All<T>(this IEnumerable<T> seq, Func<T, bool> comparer, out bool isAll)
        {
            var notAll = false;
            seq = seq.Parallell(i => notAll = !comparer(i));
            isAll = !notAll;
            return seq;
        }

        public static IEnumerable<T> Parallell<T>(this IEnumerable<T> seq, Action<T> action)
        {
            var enumerator = seq.GetEnumerator();
            T current = default(T);
            Func<bool> next = () =>
            {
                if (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    action(current);
                    return true;
                }
                return false;
            };
            return new QuickEnumerator<T>(next, () => current).ToEnumerable();
        }
    }
}