using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagCloud.Settings
{
    public class LoaderSettings : ISettings
    {
        public FileInfo FileInfo { get; set; }

        public List<char> Separators { get; set; } = new List<char>
        {
            ' ',
            '\n',
            '\t',
            '\r',
            '.',
            ':',
            ';',
            '>',
            '<',
            ',',
            '\'',
            '|',
            '/',
            '"',
            '`',
            '~',
            '!',
            '?',
            '{',
            '}',
            '[',
            ']',
            '(',
            ')',
            '-',
            '_',
            '+',
            '=',
            '@',
            '#',
            '$',
            '%',
            '*'
        };
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
        public string GetSettingsName() => "Loader";
    }
}