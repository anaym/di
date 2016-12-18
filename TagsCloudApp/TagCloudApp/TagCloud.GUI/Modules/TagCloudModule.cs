using Autofac;
using TagCloud.Core.Layouter;
using TagCloud.Core.Renderer;
using TagCloud.Core.Source;
using TagCloud.Layouter;
using TagCloud.Renderer;
using TagCloud.Source;

namespace TagCloud.GUI.Modules
{
    public class TagCloudModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdaptiveHeightExtractor>().As<IHeightExtractor>();
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