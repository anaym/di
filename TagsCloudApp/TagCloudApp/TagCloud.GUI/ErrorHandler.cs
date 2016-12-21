using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utility.RailwayExceptions;

namespace TagCloud.GUI
{
    public static class ErrorHandler
    {
        public static void OnErrorNotify<T>(this Result<T> result)
        {
            new[] { result }.OnAnyErrorNotify();
        }


        public static void OnAnyErrorNotify<T>(this IEnumerable<Result<T>> results)
        {
            
        }

        public static void OnAllErrorNotify<T>(this IEnumerable<Result<T>> results)
        {
            
        }

        public static void NotifyError(string description)
        {
            MessageBox.Show(description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void OnErrorNotify<T>(this IEnumerable<Result<T>> results, bool all)
        {
            var errors = results
                .Where(r => r.IsFail)
                .Select(r => r.Error)
                .ToList();
            if (errors.Count == 0) return;
            NotifyError(string.Join("\n", errors));
        }
    }
}