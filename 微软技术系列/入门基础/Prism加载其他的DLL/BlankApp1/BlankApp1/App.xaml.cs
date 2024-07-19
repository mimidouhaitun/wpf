using BlankApp1.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace BlankApp1
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

        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModelA.ModelAProfile>();
            moduleCatalog.AddModule<ModelB.ModelBProfile>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
