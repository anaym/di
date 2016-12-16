using System;
using System.IO;
using System.Linq;
using System.Text;
using TagCloudApp.Source;

namespace TagCloudApp.App.Actions.File
{
    public class WordsLoadUiAction : IUiAction
    {
        private readonly LoaderSettings settings;
        private readonly IFileWordsSource[] wordsSources;
        private readonly TagCollection collection;

        public WordsLoadUiAction(LoaderSettings settings, IFileWordsSource[] wordsSources, TagCollection collection)
        {
            this.settings = settings;
            this.wordsSources = wordsSources;
            this.collection = collection;
        }

        public string Category => "File";
        public string Name => "Load";
        public string Description => "Load words from file";
        public double Index => -0.1;

        public void Perform(IApplication app)
        {
            var pathes = app.RequestOpenFiles("*");
            if (pathes != null && pathes.Length > 0)
            {
                try
                {
                    settings.FileInfo = new FileInfo(pathes.First());
                    foreach (var source in wordsSources)
                    {
                        if (source.IsCanRead())
                        {
                            collection.Clear();
                            collection.AddAnyWords(source.GetWords().ToList());
                            app.DocumentFileName = settings.FileInfo.Name;
                            app.HasUnapplayedChanges = true;
                            return;
                        }
                    }
                    app.Notify($"Can`t load '{settings.FileInfo.Name}'", "Error:");
                }
                catch (Exception e)
                {
                    app.Notify($"Can`t load: {e}", "Exception!");
                }
            }
        }
    }
}