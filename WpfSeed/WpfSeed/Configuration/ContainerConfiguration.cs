using Autofac;

namespace WpfSeed.Configuration
{
    public static class ContainerConfiguration
    {
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterModule(new ViewModels.ViewModelsModule());

            return builder.Build();
        }
    }
}
