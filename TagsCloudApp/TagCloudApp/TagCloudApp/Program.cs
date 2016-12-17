using System;
using System.Windows.Forms;
using Autofac;
using TagCloudApp.GUI;
using TagCloudApp.TUI;
using Form = TagCloudApp.TUI.Forms.Form;

namespace TagCloudApp
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<TagCloudModule>();

            builder.RegisterType<GuiApplication>().As<IApplication>();
            builder.RegisterType<PictureBox>().AsSelf().SingleInstance();

            var form = new Form();
            var engine = Engine.FromConsole(form);
            engine.Run();

            var container = builder.Build();
            container.Resolve<IApplication>().Run();
        }
    }
}
