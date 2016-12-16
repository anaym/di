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
        private readonly Func<string, Encoding, IFileWordsSource>[] loaderFactories;
        private readonly TagCollection collection;

        public WordsLoadUiAction(LoaderSettings settings, Func<string, Encoding, IFileWordsSource>[] loaderFactories, TagCollection collection)
        {
            this.settings = settings;
            this.loaderFactories = loaderFactories;
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
                settings.FileName = pathes.First();
                foreach (var factory in loaderFactories)
                {
                    var loader = factory(settings.FileName, settings.Encoding);
                    if (loader.IsCanRead())
                    {
                        collection.Clear();
                        collection.AddAnyWords(loader.GetWords().ToList());
                        app.ChangeDocumentFileName(Path.GetFileName(settings.FileName));
                        app.ChangeDocumentNewStatus(true);
                        return;
                    }
                }
                app.Notify($"Can`t load {settings.FileName}");
            }
        }
    }
}