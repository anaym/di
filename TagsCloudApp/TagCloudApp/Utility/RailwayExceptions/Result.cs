using System;

namespace Utility.RailwayExceptions
{
    public struct Result<T>
    {
        private T Value { get; }
        public bool IsSuccess { get; }
        public string Exception { get; }
        public bool IsFail => !IsSuccess;

        public static Result<T> Success(T value) => new Result<T>(value, true, null);
        public static Result<T> Fail(string exception) => new Result<T>(default(T), false, exception);


        private Result(T value, bool isSuccess, string exception)
        {
            Value = value;
            IsSuccess = isSuccess;
            Exception = exception;
        }

        public T GetValueOrThrow()
        {
            if (IsFail) throw new InvalidOperationException($"No value. Only exception: {Exception}");
            return Value;
        }

        public bool TryGetValue(out T value)
        {
            value = Value;
            return IsSuccess;
        }
    }

    public static class Result
    {
        public static Result<T> Success<T>(T result) => Result<T>.Success(result);
        public static Result<None> Success() => Result.Success(new None());
        public static Result<T> Fail<T>(string exception) => Result<T>.Fail(exception);
        public static Result<None> Fail(string exception) => Result.Fail<None>(exception);
        public static Result<T> Validate<T>(T value, Func<T, bool> validator, string exception = null)
        {
            return validator(value) ? Result.Success(value) : Result.Fail<T>(exception ?? $"Invalid argument: {value}");
        }


        public static Result<T> Of<T>(Func<T> func, string error = null)
        {
            try
            {
                return Result.Success(func());
            }
            catch (Exception ex)
            {
                return Result.Fail<T>(error ?? ex.Message);
            }
        }

        public static Result<None> Of(Action func, string error = null) => Result.Of(func.ToFunc(), error);
    }
}