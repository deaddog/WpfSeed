using Autofac;
using System.Windows;
using WpfSeed.Views;

namespace WpfSeed
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = Configuration.ContainerConfiguration.CreateContainer();

            var mainViewModel = container.Resolve<ViewModels.MainViewModel>();
            var mainWindow = new MainWindow();

            MainWindow.DataContext = mainViewModel;
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
