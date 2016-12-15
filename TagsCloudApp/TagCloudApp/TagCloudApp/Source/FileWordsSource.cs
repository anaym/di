using System;
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
            return
                File.ReadAllLines(settings.FileName, settings.Encoding)
                    .SelectMany(l => l.Split(' ', '\t', '\n'))
                    .Where(w => !string.IsNullOrWhiteSpace(w));
        }
    }

    public class FileWordsSourceSettings
    {
        public string FileName { get; set; }

        public string EncodingName
        {
            get { return Encoding.EncodingName; }
            set
            {
                try
                {
                    Encoding = Encoding.GetEncoding(value);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
        public Encoding Encoding { get; private set; } = Encoding.Default;
    }
}