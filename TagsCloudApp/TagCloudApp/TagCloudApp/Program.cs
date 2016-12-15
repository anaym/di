using System;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagCloud.Core.Layouter;
using TagCloud.Core.Renderer;
using TagCloud.Core.Source;
using TagCloud.Core.Task;
using TagCloudApp.App;
using TagCloudApp.App.Actions;
using TagCloudApp.App.GUI;
using TagCloudApp.App.TUI;
using TagCloudApp.Layouter;
using TagCloudApp.Renderer;
using TagCloudApp.Source;
using TagCloudApp.Task;
using Module = Autofac.Module;

namespace TagCloudApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<TagCloudTaskModule>();

            var container = builder.Build();
            container.Resolve<IApplication>().Run();
        }
    }

    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GuiApplication>().As<IApplication>();
            builder.RegisterType<RenderSettings>().AsSelf().SingleInstance();
            builder.RegisterType<FileWordsSourceSettings>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBox>().AsSelf().SingleInstance();
            builder.RegisterType<TagCollection>().AsSelf().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).As<IUiAction>();
        }
    }

    public class TagCloudTaskModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileWordsSource>().As<IWordsSource>();
            builder.RegisterType<LowCaseTagExtractor>().As<ITagExtractor>();
            builder.RegisterType<TagLayoutTask>().As<ITagLayoutTask>();
            builder.RegisterType<AllTagFilter>().As<ITagFilter>();
            builder.RegisterType<ScaledHeightExtractor>().As<IHeightExtractor>();
            builder.RegisterType<GraphicSizeExtractor>().As<ISizeExtractor>();
            builder.RegisterType<SizeCircularLayouter>().As<ISizeLayouter>();
            builder.RegisterType<TagLayouter>().As<ITagLayouter>();
            builder.RegisterType<TagCloudRenderer>().As<ITagCloudRenderer>();
        }
    }
}
