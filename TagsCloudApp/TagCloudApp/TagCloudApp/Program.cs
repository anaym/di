using System;
using Autofac;

namespace TagCloudApp
{
    internal class Program
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
}
