using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Core.Source;
using TagCloudApp.Source;

namespace TagCloudApp.App.GUI.Actions
{
    public class WordsLoadUiAction : IUiAction
    {
        private readonly FileWordsSourceSettings settings;
        private readonly IWordsSource source;
        private readonly TagCollection collection;

        public WordsLoadUiAction(FileWordsSourceSettings settings, IWordsSource source, TagCollection collection)
        {
            this.settings = settings;
            this.source = source;
            this.collection = collection;
        }

        public string Category => "File";
        public string Name => "Load from file";
        public string Description => "Load words from file";
        public double Index => -0.1;
        public void Perform(IApplication app)
        {
            var pathes = app.RequestOpenFiles("*");
            if (pathes != null && pathes.Length > 0)
            {
                settings.FileName = pathes.First();
                collection.Clear();
                collection.AddAnyWords(source.GetWords().ToList());
            }
        }
    }

    public class WordsLoadSettingsUiAction : IUiAction
    {
        private readonly FileWordsSourceSettings settings;

        public WordsLoadSettingsUiAction(FileWordsSourceSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "File";
        public string Name => "File load settings";
        public string Description => "Settings for loading";
        public double Index => -0.1;
        public void Perform(IApplication app)
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}