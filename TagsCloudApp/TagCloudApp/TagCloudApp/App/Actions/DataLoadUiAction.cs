using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloudApp.IO;
using TagCloudApp.Layouter;
using TagCloudApp.WordToTag;

namespace TagCloudApp.App.GUI.Actions
{
    public class DataLoadUiAction : IUiAction
    {
        private readonly FileWordsSourceSettings settings;
        private readonly IWordsSource source;
        private readonly TagCollection frequences;

        public DataLoadUiAction(FileWordsSourceSettings settings, IWordsSource source, TagCollection frequences)
        {
            this.settings = settings;
            this.source = source;
            this.frequences = frequences;
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
                frequences.Clear();
                frequences.AddAnyWords(source.GetWords());
                app.Rerender();
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