using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Conventions;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var kernel = new StandardKernel();
                //Биндим все реализации интерфейса из текущей сборки
                kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom<IUiAction>().BindAllInterfaces());
                kernel.Bind<IObjectSerializer>().To<XmlObjectSerializer>().InSingletonScope();
                kernel.Bind<IBlobStorage>().To<FileBlobStorage>().InSingletonScope();
                kernel.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
                kernel.Bind<Palette>().ToSelf().InSingletonScope();
                //ToConst - плоха идея, ибо тогда мы пользуемся контейнером до завершения конфигурироания
                kernel.Bind<AppSettings>().ToMethod(c => c.Kernel.Get<SettingsManager>().Load()).InSingletonScope();
                //Автогенерируемая фабрика (из аргументов метода Create будут взяты аргументы для конструктора. Недостающие получим методом kernel.Get)
                kernel.Bind<IDragonPainterFactory>().ToFactory();
                //Ручная привязка фабрики
                kernel.Bind<IDragonSettingsFactory>().To<DragonSettingsGenerator>();
                kernel.Bind<ImageSettings>().ToMethod(c => c.Kernel.Get<AppSettings>().ImageSettings);
                kernel.Bind<string>()
                    .ToMethod(c => c.Kernel.Get<AppSettings>().ImagesDirectory)
                    //.WhenInjectedExactlyInto<SaveImageAction>(); //Имя типа в который подставляем
                    .When(c => c.Target?.Name == "savePath"); //Имя параметра к которому подставляем

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(kernel.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
}
    }

    public class NewAction : IUiAction
    {
        public string Category => "NC";
        public string Name => "HIII";
        public string Description => "Show hi";
        public double Index => 100500;
        public void Perform()
        {
            MessageBox.Show("Hi!");
        }
    }
}