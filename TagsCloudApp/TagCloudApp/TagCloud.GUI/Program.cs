using System;
using System.Windows.Forms;
using Autofac;
using TagCloud.GUI.Modules;

namespace TagCloud.GUI
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<TagCloudModule>();

            var container = builder.Build();
            container.Resolve<MainForm>().Run();
        }
    }
}
