using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BlankApp1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public DelegateCommand<string> OpenCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            OpenCommand = new DelegateCommand<string>(Open);
        }

        public void Open(string param)
        {
            _regionManager.Regions["ContentRegion"].RequestNavigate(param);
        }
    }
}
