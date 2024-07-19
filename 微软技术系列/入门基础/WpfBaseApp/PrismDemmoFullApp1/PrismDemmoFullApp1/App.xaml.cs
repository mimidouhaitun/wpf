using Prism.Ioc;
using Prism.Modularity;
using PrismDemmoFullApp1.Modules.ModuleName;
using PrismDemmoFullApp1.Services;
using PrismDemmoFullApp1.Services.Interfaces;
using PrismDemmoFullApp1.Views;
using System.Windows;

namespace PrismDemmoFullApp1
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
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
