using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TagCloud.Core.Source;

namespace TagCloudApp.Source
{
    public class FileWordsSource : IWordsSource
    {
        private readonly FileWordsSourceSettings settings;

        public FileWordsSource(FileWordsSourceSettings settings)
        {
            this.settings = settings;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(settings.FileName, settings.Encoding).SelectMany(l => l.Split(' ', '\t', '\n')).Where(w => !string.IsNullOrWhiteSpace(w));
        }
    }

    public class FileWordsSourceSettings
    {
        public string FileName { get; set; }
        public Encoding Encoding { get; set; } = Encoding.Default;
    }
}