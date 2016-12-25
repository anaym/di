using System;

namespace Utility.RailwayExceptions.Extensions
{
    public static class ExceptionUtilityExtension
    {
        public static Result<T> OnFail<T>(this Result<T> result, Action<Result<T>> handler)
        {
            if (result.IsFail) handler(result);
            return result;
        }
        public static Result<T> OnFail<T>(this Result<T> result, Action handler) => result.OnFail(e => handler());

        public static Result<T> ReThrowException<T>(this Result<T> result)
        {
            return result.OnFail(e => { throw e.Exception; });
        }

        public static Result<T> ReplaceException<T>(this Result<T> result, Func<Exception, Exception> exceptionReplacer)
        {
            if (result.IsSuccess) return result;
            return Results.Fail<T>(exceptionReplacer(result.Exception));
        }

        public static Result<T> RefineException<T>(this Result<T> result, string additionalInfo) => result.ReplaceException(e => new InvalidOperationException($"{additionalInfo}", e));
    }
}