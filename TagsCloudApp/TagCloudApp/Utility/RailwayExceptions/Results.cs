using System;

namespace Utility.RailwayExceptions
{
    public static class Results
    {
        public static Result<T> Success<T>(T result) => Result<T>.Success(result);
        public static Result<None> Success() => Results.Success(new None());
        public static Result<T> Fail<T>(Exception exception) => Result<T>.Fail(exception);
        public static Result<None> Fail(Exception exception) => Results.Fail<None>(exception);

        public static Result<T> Validate<T>(T value, Func<T, bool> validator, string exceptionMessage)
        {
            return Results.Validate(value, validator, new ArgumentException(exceptionMessage));
        }
        public static Result<T> Validate<T>(T value, Func<T, bool> validator, Exception exception = null)
        {
            exception = exception ?? new ArgumentException($"{value} is not valid");
            return validator(value) ? Results.Success(value) : Results.Fail<T>(exception);
        }


        public static Result<T> Of<T>(Func<T> func)
        {
            try
            {
                return Results.Success(func());
            }
            catch (Exception ex)
            {
                var exception = new InvalidOperationException("Invalid operation", ex);
                return Results.Fail<T>(exception);
            }
        }

        public static Result<None> Of(Action func) => Results.Of(func.ToFunc());
    }
}