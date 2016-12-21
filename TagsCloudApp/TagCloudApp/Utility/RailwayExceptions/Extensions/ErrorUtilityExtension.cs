using System;

namespace Utility.RailwayExceptions.Extensions
{
    //TODO: ВОПРОС:
    //Насколько хорошо создавать такие Extension классы? Не лучше ли добавить эти методы в основной класс, если уж у меня есть к нему доступ?
    //Наставник говорит - что это плохо
    //Я считаю - хорошо, ибо не загромождает исходный класс дополнительным функционалом. Так же такой подход используется в примерах.
    //Так как правильно?
    public static class ErrorUtilityExtension
    {
        public static Result<T> OnFail<T>(this Result<T> result, Action<Result<T>> handler)
        {
            if (result.IsFail) handler(result);
            return result;
        }
        public static Result<T> OnFail<T>(this Result<T> result, Action handler) => result.OnFail(e => handler());

        public static Result<T> ReThrowException<T>(this Result<T> result, Func<string, Exception> exceptionFactory)
        {
            return result.OnFail(e => { throw exceptionFactory(e.Error); });
        }
        public static Result<T> ReThrowException<T>(this Result<T> result) => result.ReThrowException(e => new InvalidOperationException(e));

        public static Result<T> ReplaceError<T>(this Result<T> result, Func<string, string> errorReplacer)
        {
            if (result.IsSuccess) return result;
            return Result.Fail<T>(errorReplacer(result.Error));
        }
        public static Result<T> ReplaceError<T>(this Result<T> result, string newException) => result.ReplaceError(e => newException);
        public static Result<T> RefineError<T>(this Result<T> result, string additionalInfo) => result.ReplaceError(e => $"{additionalInfo}. {e}");
    }
}