namespace TagCloud.Settings
{
    public class LayouterSettings : ISettings
    {
        public int WidthScale { get; set; } = 2;
        public int HeightScale { get; set; } = 1;
        public int MinCharHeight { get; set; } = 8;
        public int MaxCharHeight { get; set; } = 128;
        public string SettingsName => "Layouter";
    }
}