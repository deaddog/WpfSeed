using Autofac;
using WpfSeed.ViewModels;

namespace WpfSeed
{
    public class ViewModelLocator
    {
        private static IContainer CreateDesignContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ViewModelsModule());

            return builder.Build();
        }

        private readonly IContainer _container;

        public ViewModelLocator()
        {
            _container = CreateDesignContainer();
        }

        public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();
    }
}
