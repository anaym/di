using System.IO;
using System.Windows.Forms;
using FractalPainting.Infrastructure;

namespace FractalPainting.App.Actions
{
	public class SaveImageAction : IUiAction
	{
		private readonly string savePath;
		private readonly IImageHolder imageHolder;

	    public SaveImageAction(string savePath, IImageHolder imageHolder)
	    {
	        this.savePath = savePath;
	        this.imageHolder = imageHolder;
	    }


	    public string Category => "Файл";
		public string Name => "Сохранить...";
		public string Description => "Сохранить изображение в файл";
	    public double Index => 0;

	    public void Perform()
		{
			var dialog = new SaveFileDialog
			{
				CheckFileExists = false,
				InitialDirectory = Path.GetFullPath(savePath),
                DefaultExt = "bmp",
                FileName = "image.bmp",
                Filter = "Изображения (*.bmp)|*.bmp" 
			};
			var res = dialog.ShowDialog();
			if (res == DialogResult.OK)
				imageHolder.SaveImage(dialog.FileName);
		}
	}
}