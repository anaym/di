using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App.Actions
{
	public class KochFractalAction : IUiAction
	{
	    private readonly KochPainter painter;

	    public KochFractalAction(KochPainter painter)
	    {
	        this.painter = painter;
	    }

		public string Category => "Фракталы";
		public string Name => "Кривая Коха";
		public string Description => "Кривая Коха";
	    public double Index => 1;

	    public void Perform()
		{
            painter.Paint();
		}
	}
}