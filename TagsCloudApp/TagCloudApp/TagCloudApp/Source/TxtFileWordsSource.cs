using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagCloudApp.Source
{
    public class TxtFileWordsSource : IFileWordsSource
    {
        private readonly string name;
        private readonly Encoding encoding;

        public TxtFileWordsSource(string name, Encoding encoding)
        {
            this.name = name;
            this.encoding = encoding;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(name, encoding)
                .SelectMany(l => l.Split(' ', '\t', '\n'))
                .Where(w => !string.IsNullOrWhiteSpace(w));
        }

        public bool IsCanRead()
        {
            return name.EndsWith(".txt");
        }
    }
}