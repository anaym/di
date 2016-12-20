using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.RailwayExceptions.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> GetSuccesful<T>(this IEnumerable<Result<T>> results)
        {
            return results.Where(i => i.IsSuccess).Select(i => i.GetValueOrThrow());
        }

        public static KeyValuePair<TK, TV> GetValueOrThrow<TK, TV>(this KeyValuePair<Result<TK>, Result<TV>> pair)
        {
            return new KeyValuePair<TK, TV>(pair.Key.GetValueOrThrow(), pair.Value.GetValueOrThrow());
        }

        public static bool IsSuccess<TK, TV>(this KeyValuePair<Result<TK>, Result<TV>> pair)
        {
            return pair.Value.IsSuccess && pair.Key.IsSuccess;
        }

        public static IEnumerable<KeyValuePair<TK, TV>> GetSuccesful<TK, TV>(this IEnumerable<KeyValuePair<Result<TK>, Result<TV>>> results)
        {
            return results.Where(IsSuccess).Select(GetValueOrThrow);
        }
    }
}
