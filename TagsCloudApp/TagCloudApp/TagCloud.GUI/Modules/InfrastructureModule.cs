using Autofac;
using TagCloud.Settings;
using Module = Autofac.Module;

namespace TagCloud.GUI.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoaderSettings>().AsSelf().As<ISettings>().SingleInstance();
            builder.RegisterType<LayouterSettings>().AsSelf().As<ISettings>().SingleInstance();
            builder.RegisterType<RendererSettings>().AsSelf().As<ISettings>().SingleInstance();
            builder.RegisterType<TagCollection>().AsSelf().SingleInstance();
            builder.RegisterType<TagCloudCreator>().AsSelf();
            builder.RegisterType<MainForm>().AsSelf();
        }
    }
}