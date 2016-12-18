using Autofac;
using TagCloud.Settings;
using Module = Autofac.Module;

namespace TagCloud.GUI.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoaderSettings>().AsSelf().SingleInstance();
            builder.RegisterType<LayouterSettings>().AsSelf().SingleInstance();
            builder.RegisterType<RendererSettings>().AsSelf().SingleInstance();
            builder.RegisterType<TagCollection>().AsSelf().SingleInstance();
            builder.RegisterAssemblyTypes(typeof(ISettings).Assembly).As<ISettings>().SingleInstance();
            builder.RegisterType<TagCloudCreator>().AsSelf();
            builder.RegisterType<MainForm>().AsSelf();
        }
    }
}