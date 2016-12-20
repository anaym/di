using System;

namespace Utility.RailwayExceptions.Extensions
{
    //TODO: ВОПРОС:
    //Насколько хорошо создавать такие Extension классы? Не лучше ли добавить эти методы в основной класс, если уж у меня есть к нему доступ?
    //Наставник говорит - что это плохо
    //Я считаю - хорошо, ибо не загромождает исходный класс дополнительным функционалом. Так же такой подход используется в примерах.
    //Так как правильно?
    public static class ExceptionsUtilityExtension
    {
        public static Result<T> OnFail<T>(this Result<T> result, Action<string> handler)
        {
            if (result.IsFail) handler(result.Exception);
            return result;
        }
        public static Result<T> OnFail<T>(this Result<T> result, Action handler) => result.OnFail(e => handler());

        public static Result<T> ReThrowException<T>(this Result<T> result, Func<string, Exception> exceptionFactory)
        {
            return result.OnFail(e => { throw exceptionFactory(e); });
        }
        public static Result<T> ReThrowException<T>(this Result<T> result) => result.ReThrowException(e => new InvalidOperationException(e));

        public static Result<T> ReplaceException<T>(this Result<T> result, Func<string, string> errorReplacer)
        {
            if (result.IsSuccess) return result;
            return Result.Fail<T>(errorReplacer(result.Exception));
        }
        public static Result<T> ReplaceException<T>(this Result<T> result, string newException) => result.ReplaceException(e => newException);
        public static Result<T> RefineException<T>(this Result<T> result, string additionalInfo) => result.ReplaceException(e => $"{additionalInfo}. {e}");
    }
}