using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility.RailwayExceptions.Extensions
{
    public static class UtilityExtension
    {
        public static Result<T> Unpack<T>(this Result<Result<T>> result)
        {
            return result.IsFail ? Result<T>.Fail(result.Exception) : result.GetValueOrThrow();
        }

        public static Result<T> Validate<T>(this Result<T> result, Func<T, bool> validator, string exception = null)
        {
            return result.Select(t => Results.Validate(t, validator, exception)).Unpack();
        }

        public static Result<bool> Validate(this Result<bool> result) => result.Validate(r => r);

        public static Result<None> IgnoreValue<T>(this Result<T> result)
        {
            return result.Select(v => new None());
        }

        public static IEnumerable<Result<TR>> Select<T, TR>(this IEnumerable<Result<T>> seq, Func<T, TR> selector)
        {
            return seq.Select(i => i.Select(selector));
        }

        public static IEnumerable<Result<None>> Select<T>(this IEnumerable<Result<T>> seq, Action<T> action)
        {
            return seq.Select(action.ToFunc());
        }

        public static Result<TD> Cast<TS, TD>(this Result<TS> result) where TS : TD
        {
            return result.Select(r => (TD)r);
        }
    }
}