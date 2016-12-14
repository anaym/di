using System;
using FractalPainting.Infrastructure;
using Ninject.Activation;

namespace FractalPainting.App
{
    public class AppSettings
    {
        public string ImagesDirectory { get; set; }
        public ImageSettings ImageSettings { get; set; }
    }
}