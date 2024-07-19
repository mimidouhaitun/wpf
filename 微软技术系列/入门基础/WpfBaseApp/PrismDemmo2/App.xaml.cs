using Prism.Ioc;
using PrismDemmo2.Views;
using System.Windows;

namespace PrismDemmo2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UserControlA>();
            containerRegistry.RegisterForNavigation<UserControlB>();
            containerRegistry.RegisterForNavigation<UserControlC>();
        }
    }
}
