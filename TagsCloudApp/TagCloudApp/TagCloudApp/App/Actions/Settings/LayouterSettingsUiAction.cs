using TagCloudApp.Layouter;

namespace TagCloudApp.App.Actions.Settings
{
    public class LayouterSettingsUiAction : IUiAction
    {
        private readonly LayouterSettings settings;

        public LayouterSettingsUiAction(LayouterSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Settings";
        public string Name => "Layouter";
        public string Description => "Layouter settings";
        public double Index => 1.5;
        public void Perform(IApplication app)
        {
            app.Request(settings);
            app.ChangeDocumentNewStatus(true);
        }
    }
}