using System;

namespace Utility.RailwayExceptions.Extensions
{
    public static class ExecuteExtension
    {
        public static Result<T> Execute<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess) action(result.GetValueOrThrow());
            return result;
        }
        public static Result<T> Execute<T>(this Result<T> result, Action action) => result.Execute(_ => action());
        public static Result<None> Execute(this Result<None> result, Action foo) => result.Execute<None>(foo);
    }
}