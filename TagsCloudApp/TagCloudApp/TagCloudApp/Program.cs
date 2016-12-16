using System;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagCloud.Core.Layouter;
using TagCloud.Core.Renderer;
using TagCloud.Core.Source;
using TagCloudApp.App;
using TagCloudApp.App.Actions;
using TagCloudApp.App.GUI;
using TagCloudApp.Layouter;
using TagCloudApp.Renderer;
using TagCloudApp.Source;
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
            builder.RegisterType<LoaderSettings>().AsSelf().SingleInstance();
            builder.RegisterType<LayouterSettings>().AsSelf().SingleInstance();
            builder.RegisterType<RendererSettings>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBox>().AsSelf().SingleInstance();
            builder.RegisterType<TagCollection>().AsSelf().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).As<IUiAction>();
        }
    }

    public class TagCloudTaskModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdaptiveHeightExtractor>()
                .WithParameter((p, c) => p.Name == "minCharHeight", (p, c) => c.Resolve<LayouterSettings>().MinCharHeight)
                .WithParameter((p, c) => p.Name == "maxCharHeight", (p, c) => c.Resolve<LayouterSettings>().MaxCharHeight)
                .WithParameter((p, c) => p.Name == "minTagFrequence", (p, c) => c.Resolve<TagCollection>().MinFrequence)
                .WithParameter((p, c) => p.Name == "maxTagFrequence", (p, c) => c.Resolve<TagCollection>().MaxFrequence)
                .As<IHeightExtractor>();

            builder.RegisterType<TxtFileWordsSource>().As<IFileWordsSource>();
            builder.RegisterType<LowCaseTagExtractor>().As<ITagExtractor>();
            builder.RegisterType<AllTagFilter>().As<ITagFilter>();
            builder.RegisterType<GraphicSizeExtractor>().As<ISizeExtractor>();
            builder.RegisterType<SizeCircularLayouter>().As<ISizeLayouter>();
            builder.RegisterType<TagLayouter>().As<ITagLayouter>();
            builder.RegisterType<TagCloudRenderer>().As<ITagCloudRenderer>();
        }
    }
}
