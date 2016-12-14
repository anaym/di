using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App.Actions
{
	public class DragonFractalAction : IUiAction
	{
	    private readonly IDragonPainterFactory dragonPainterFactory;
	    private readonly IDragonSettingsFactory dragonSettingsFactory;

	    public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, IDragonSettingsFactory dragonSettingsFactory)
	    {
	        this.dragonPainterFactory = dragonPainterFactory;
	        this.dragonSettingsFactory = dragonSettingsFactory;
	    }

	    public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";
	    public double Index => 1;

	    public void Perform()
	    {
	        var dragonSettings = dragonSettingsFactory.CreateDragonSettings();
	        // редактируем настройки:
	        SettingsForm.For(dragonSettings).ShowDialog();
	        // создаём painter с такими настройками
	        dragonPainterFactory.CreateDragonPainter(dragonSettings).Paint();
	    }
	}
}