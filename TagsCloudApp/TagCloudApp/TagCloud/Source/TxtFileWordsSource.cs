using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Core.Source;
using TagCloud.Settings;
using Utility;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace TagCloud.Source
{
    public class TxtFileWordsSource : IWordsSource
    {
        private readonly LoaderSettings settings;

        public TxtFileWordsSource(LoaderSettings settings)
        {
            this.settings = settings;
        }

        public Result<IEnumerable<string>> GetWords()
        {
            return Results
                .Validate(settings.FileInfo, fi => fi.Extension == ".txt", "Not supported file format")
                .Select(fi => File.ReadAllLines(fi.FullName, settings.Encoding))
                .Select(lines => lines.SelectMany(l => l.Split(settings.Separators.ToArray())))
                .Select(parts => parts.WhereNot(string.IsNullOrWhiteSpace));
        }
    }
}