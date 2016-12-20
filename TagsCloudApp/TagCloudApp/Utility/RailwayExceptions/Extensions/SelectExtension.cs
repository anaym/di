using System;

namespace Utility.RailwayExceptions.Extensions
{
    public static class SelectExtension
    {
        public static Result<TR> Select<T, TR>(this Result<T> result, Func<T, TR> selector)
        {
            if (result.IsFail) return Result.Fail<TR>(result.Exception);
            return Result.Of(() => selector(result.GetValueOrThrow()));
        }
        public static Result<None> Select<T>(this Result<T> result, Action<T> action) => result.Select(action.ToFunc());
        public static Result<None> Select(this Result<None> result, Action foo) => result.Select(a => foo());
    }
}
