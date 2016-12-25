using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utility.RailwayExceptions;

namespace TagCloud.GUI
{
    public static class ExceptionHandler
    {
        public static Result<T> OnExceptionNotify<T>(this Result<T> result)
        {
            new[] { result }.OnAnyExceptionNotify();
            return result;
        }


        public static Result<None> OnAnyExceptionNotify<T>(this IEnumerable<Result<T>> results)
        {
            return results.ToList().OnExceptionNotify(false);
        }

        public static Result<None> OnAllExceptionNotify<T>(this IEnumerable<Result<T>> results)
        {
            return results.ToList().OnExceptionNotify(true);
        }

        public static void NotifyException(string description)
        {
            MessageBox.Show(description, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static Result<None> OnExceptionNotify<T>(this List<Result<T>> results, bool all)
        {
            var exceptions = results
                .Where(r => r.IsFail)
                .Select(r => r.Exception)
                .Select(ExtractFullMessage)
                .ToList();
            if (exceptions.Count == 0) return Results.Success();
            if (exceptions.Count < results.Count && all) return Results.Success();
            NotifyException(string.Join("\n", exceptions));
            return Results.Fail(new InvalidOperationException());
        }

        private static string ExtractFullMessage(Exception ex)
        {
            if (ex == null) return "";
            return $"{ex.Message}. {ExtractFullMessage(ex.InnerException)}";
        }
    }
}