using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TagCloudApp.IO;
using TagCloudApp.WordToTag;

namespace TagCloudApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestWordSource>().As<IWordsSource>();
            builder.RegisterType<TestWordsToTag>().As<IWordsToTag>();

            var container = builder.Build();
            foreach (var w in container.Resolve<IWordsSource>().GetWords())
            {
                Console.WriteLine(w);
            }
        }
    }
}
