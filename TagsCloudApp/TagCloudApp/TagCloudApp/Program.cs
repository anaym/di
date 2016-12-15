using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;
using TagCloudApp.App;
using TagCloudApp.App.GUI;
using TagCloudApp.App.GUI.Actions;
using TagCloudApp.HeightExtractor;
using TagCloudApp.IO;
using TagCloudApp.Layouter;
using TagCloudApp.SizeExtractor;
using TagCloudApp.TagCloudRender;
using TagCloudApp.WordToTag;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloudApp
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestWordSource>().As<IWordsSource>();
            builder.RegisterType<FileWordsSourceSettings>().AsSelf().SingleInstance();
            builder.RegisterType<TestPngImageDestination>().As<IImageDestination>();
            builder.RegisterType<LowCaseTagExtractor>().As<ITagExtractor>();
            builder.RegisterType<AutomaticTagLayoutTask>().As<ITagLayoutTask>();
            builder.RegisterType<AllTagFilter>().As<ITagFilter>();
            builder.RegisterType<ScaledHeightExtractor>().As<IHeightExtractor>();
            builder.RegisterType<GraphicSizeExtractor>().As<ISizeExtractor>();
            builder.RegisterType<SizeCircularLayouter>().As<ISizeLayouter>();
            builder.RegisterType<TagLayouter>().As<ITagLayouter>();
            builder.RegisterType<TagCloudRenderer>().As<ITagCloudRenderer>();
            builder.RegisterType<GuiApplication>().As<IApplication>();
            builder.RegisterType<RenderSettings>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBox>().AsSelf().SingleInstance();
            builder.RegisterType<Dictionary<string, Rectangle>>().AsSelf().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).As<IUiAction>();


            var container = builder.Build();
            container.Resolve<IApplication>().Run();
        }
    }
}
