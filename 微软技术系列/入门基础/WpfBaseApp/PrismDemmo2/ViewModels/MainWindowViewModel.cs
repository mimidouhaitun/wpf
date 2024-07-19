using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace PrismDemmo2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> OpenComman { get; private set; }

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            OpenComman = new DelegateCommand<string>(Open);
            _regionManager = regionManager;
        }

        private void Open(string obj)
        {
            //这里的obj可能的值是UserControlA/B/C ,相当于App.xaml.cs中依赖注入的对象
            _regionManager.Regions["ContentRegion"].RequestNavigate(obj);  //相当于自动给隐藏的属性赋值，并绑定到了UI页面
        }
    }
}
