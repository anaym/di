using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloudApp.IO;

namespace TagCloudApp.App.GUI.Actions
{
    public class WordsLoadUiAction : IUiAction
    {
        private readonly FileWordsSourceSettings settings;
        private readonly IWordsSource source;

        public WordsLoadUiAction(FileWordsSourceSettings settings, IWordsSource source)
        {
            this.settings = settings;
            this.source = source;
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