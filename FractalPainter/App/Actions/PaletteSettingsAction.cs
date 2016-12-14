using FractalPainting.Infrastructure;

namespace FractalPainting.App.Actions
{
	public class PaletteSettingsAction : IUiAction
	{
		private readonly Palette palette;

	    public PaletteSettingsAction(Palette palette)
	    {
	        this.palette = palette;
	    }

		public string Category => "Настройки";
		public string Name => "Палитра...";
		public string Description => "Цвета для рисования фракталов";
	    public double Index => 2;

	    public void Perform()
		{
			SettingsForm.For(palette).ShowDialog();
		}
	}
}