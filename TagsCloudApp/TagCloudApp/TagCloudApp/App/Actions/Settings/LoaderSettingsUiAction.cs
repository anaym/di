using TagCloudApp.App.GUI;
using TagCloudApp.Source;

namespace TagCloudApp.App.Actions.Settings
{
    public class LoaderSettingsUiAction : IUiAction
    {
        private readonly LoaderSettings settings;

        public LoaderSettingsUiAction(LoaderSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Settings";
        public string Name => "Loader";
        public string Description => "Settings for loading";
        public double Index => 1;
        public void Perform(IApplication app)
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}