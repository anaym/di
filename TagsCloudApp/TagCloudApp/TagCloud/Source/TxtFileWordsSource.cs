using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Core.Source;

namespace TagCloud.Source
{
    public class TxtFileWordsSource : IFileWordsSource
    {
        private readonly LoaderSettings settings;

        public TxtFileWordsSource(LoaderSettings settings)
        {
            this.settings = settings;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(settings.FileInfo.FullName, settings.Encoding)
                .SelectMany(l => l.Split(settings.Separators.ToArray()))
                .Where(w => !string.IsNullOrWhiteSpace(w));
        }

        public bool IsCanRead()
        {
            return settings.FileInfo.Extension == ".txt";
        }
    }
}