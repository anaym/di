using TagCloudApp.TagCloudRender;

namespace TagCloudApp.App.GUI.Actions
{
    public class RenderSettingsUiAction : IUiAction
    {
        private readonly RenderSettings settings;

        public RenderSettingsUiAction(RenderSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Render";
        public string Name => "Settings";
        public string Description => "Render tag cloud";
        public double Index => 1;
        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}