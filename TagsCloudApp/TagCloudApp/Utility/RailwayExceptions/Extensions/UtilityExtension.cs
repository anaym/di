using System;

namespace Utility.RailwayExceptions.Extensions
{
    public static class UtilityExtension
    {
        public static Result<T> Unpack<T>(this Result<Result<T>> result)
        {
            return result.IsFail ? Result<T>.Fail(result.Error) : result.GetValueOrThrow();
        }

        public static Result<T> Validate<T>(this Result<T> result, Func<T, bool> validator, string exception = null)
        {
            return result.Select(t => Result.Validate<T>(t, validator, exception)).Unpack();
        }

        public static Result<bool> Validate(this Result<bool> result) => result.Validate(r => r);

        public static Result<None> IgnoreValue<T>(this Result<T> result)
        {
            return result.Select(v => new None());
        }
    }
}