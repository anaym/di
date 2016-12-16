using TagCloudApp.Renderer;

namespace TagCloudApp.App.Actions.Settings
{
    public class RendererSettingsUiAction : IUiAction
    {
        private readonly RendererSettings settings;

        public RendererSettingsUiAction(RendererSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Settings";
        public string Name => "Renderer";
        public string Description => "Render settings";
        public double Index => 1.7;
        public void Perform(IApplication app)
        {
            app.Request(settings);
            app.HasUnapplayedChanges = true;
        }
    }
}