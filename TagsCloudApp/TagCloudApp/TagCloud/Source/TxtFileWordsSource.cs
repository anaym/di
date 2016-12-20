using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Core.Source;
using TagCloud.Settings;
using Utility.RailwayExceptions;

namespace TagCloud.Source
{
    public class TxtFileWordsSource : IFileWordsSource
    {
        private readonly LoaderSettings settings;

        public TxtFileWordsSource(LoaderSettings settings)
        {
            this.settings = settings;
        }

        public Result<IEnumerable<string>> GetWords()
        {
            return Result<IEnumerable<string>>.Success(File.ReadAllLines(settings.FileInfo.FullName, settings.Encoding)
                .SelectMany(l => l.Split(settings.Separators.ToArray()))
                .Where(w => !string.IsNullOrWhiteSpace(w)));
        }

        public Result<bool> IsCanRead()
        {
            return Result.Success(settings.FileInfo.Extension == ".txt");
        }
    }
}