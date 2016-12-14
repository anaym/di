using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using TagCloudApp.HeightExtractor;
using TagCloudApp.IO;
using TagCloudApp.Layouter;
using TagCloudApp.SizeExtractor;
using TagCloudApp.TagCloudRender;
using TagCloudApp.WordToTag;

namespace TagCloudApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestWordSource>().As<IWordsSource>();
            builder.RegisterType<TestPngImageDestination>().As<IImageDestination>();
            builder.RegisterType<LowCaseTagExtractor>().As<ITagExtractor>();
            builder.RegisterType<AutomaticTagLayoutTask>().As<ITagLayoutTask>();
            builder.RegisterType<AllTagFilter>().As<ITagFilter>();
            builder.RegisterType<ScaledHeightExtractor>().As<IHeightExtractor>();
            builder.RegisterType<GraphicSizeExtractor>().As<ISizeExtractor>();
            builder.RegisterType<SizeCircularLayouter>().As<ISizeLayouter>();
            builder.RegisterType<TagLayouter>().As<ITagLayouter>();
            builder.RegisterType<TagCloudRenderer>().As<ITagCloudRenderer>();

            try
            {
                var container = builder.Build();
                var task = container.Resolve<ITagLayoutTask>();
                var destination = container.Resolve<IImageDestination>();
                var bitmap = task.Solve();
                destination.Save(bitmap);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex.InnerException;
            }
        }
    }
}
