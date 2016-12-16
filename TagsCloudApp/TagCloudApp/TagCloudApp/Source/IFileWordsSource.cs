using System;
using System.Text;
using TagCloud.Core.Source;

namespace TagCloudApp.Source
{
    public interface IFileWordsSource : IWordsSource
    {
        bool IsCanRead();
    }

    public class LoaderSettings
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