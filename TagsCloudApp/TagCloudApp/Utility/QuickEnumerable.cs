using System.Collections;
using System.Collections.Generic;

namespace Utility
{
    public class QuickEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerator<T> enumerator;

        public QuickEnumerable(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class EnumeratorExtension
    {
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            return new QuickEnumerable<T>(enumerator);
        }
    }
}