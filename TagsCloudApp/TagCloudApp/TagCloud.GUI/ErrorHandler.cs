using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utility.RailwayExceptions;

namespace TagCloud.GUI
{
    public static class ErrorHandler
    {
        public static Result<T> OnErrorNotify<T>(this Result<T> result)
        {
            new[] { result }.OnAnyErrorNotify();
            return result;
        }


        public static Result<None> OnAnyErrorNotify<T>(this IEnumerable<Result<T>> results)
        {
            return results.ToList().OnErrorNotify(false);
        }

        public static Result<None> OnAllErrorNotify<T>(this IEnumerable<Result<T>> results)
        {
            return results.ToList().OnErrorNotify(true);
        }

        public static void NotifyError(string description)
        {
            MessageBox.Show(description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static Result<None> OnErrorNotify<T>(this List<Result<T>> results, bool all)
        {
            var errors = results
                .Where(r => r.IsFail)
                .Select(r => r.Error)
                .ToList();
            if (errors.Count == 0) return Result.Success();
            if (errors.Count < results.Count && all) return Result.Success();
            NotifyError(string.Join("\n", errors));
            return Result.Fail("Not all success");
        }
    }
}