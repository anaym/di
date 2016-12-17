using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagCloud;
using TagCloud.Layouter;
using TagCloud.Renderer;
using TagCloud.Source;
using TagCloudApp.Actions;
using TagCloudApp.GUI;
using Module = Autofac.Module;

namespace TagCloudApp
{
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
}