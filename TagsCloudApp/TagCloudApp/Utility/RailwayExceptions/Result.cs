using System;

namespace Utility.RailwayExceptions
{
    public struct Result<T>
    {
        private T Value { get; }
        public bool IsSuccess { get; }
        public Exception Exception { get; }
        public bool IsFail => !IsSuccess;

        public static Result<T> Success(T value) => new Result<T>(value, true, null);
        public static Result<T> Fail(Exception exception) => new Result<T>(default(T), false, exception);


        private Result(T value, bool isSuccess, Exception exception)
        {
            if (!isSuccess && exception == null)
                throw new ArgumentNullException(nameof(exception));

            Value = value;
            IsSuccess = isSuccess;
            Exception = exception;
        }

        public T GetValueOrThrow()
        {
            if (IsFail) throw new InvalidOperationException("No value. Only exception", Exception);
            return Value;
        }

        public bool TryGetValue(out T value)
        {
            value = Value;
            return IsSuccess;
        }

        public override string ToString() => $"<{(IsSuccess ? Value.ToString() : Exception.ToString())}>";
    }
}