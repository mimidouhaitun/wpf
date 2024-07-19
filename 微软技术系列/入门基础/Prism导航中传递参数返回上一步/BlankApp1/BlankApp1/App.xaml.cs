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
        protected override IModuleCatalog CreateModuleCatalog()
        {
            //需要在生成的目录下创建文件夹Modules，然后将ModelA和ModelB中生成的dll复制到该文件夹中，运行即可
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
    }
}
