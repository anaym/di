using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagCloudApp.Source
{
    public class TxtFileWordsSource : IFileWordsSource
    {
        private readonly FileInfo file;
        private readonly Encoding encoding;

        public TxtFileWordsSource(FileInfo file, Encoding encoding)
        {
            this.file = file;
            this.encoding = encoding;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(file.FullName, encoding)
                .SelectMany(l => l.Split(' ', '\t', '\n'))
                .Where(w => !string.IsNullOrWhiteSpace(w));
        }

        public bool IsCanRead()
        {
            return file.Extension == ".txt";
        }
    }
}