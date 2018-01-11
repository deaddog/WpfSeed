using Autofac;

namespace WpfSeed.ViewModels
{
    public class ViewModelsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var viewModelType = typeof(MainViewModel);
            var viewModelAssembly = viewModelType.Assembly;

            builder.RegisterAssemblyTypes(viewModelAssembly)
                .InNamespace(viewModelType.Namespace)
                .Where(t => t.Name.EndsWith("ViewModel"));
        }
    }
}
