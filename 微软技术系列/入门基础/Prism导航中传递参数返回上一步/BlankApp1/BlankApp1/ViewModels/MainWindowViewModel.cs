using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BlankApp1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal _navigationJournal;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public DelegateCommand<string> OpenCommand { get; private set; }
        public DelegateCommand GoBack { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            OpenCommand = new DelegateCommand<string>(Open);
            GoBack = new DelegateCommand(Back);
        }

        public void Open(string param)
        {
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("Title", "Hello!");  //传递参数
            _regionManager.Regions["ContentRegion"].RequestNavigate(param, CallBack, navigationParameters);
        }

        /// <summary>
        /// 回调方法中获得记忆
        /// </summary>
        /// <param name="result"></param>
        private void CallBack(NavigationResult result)
        {
            if ((bool)result.Result)
            {
                _navigationJournal = result.Context.NavigationService.Journal;
            }
        }

        /// <summary>
        /// 导航记忆返回上一步
        /// </summary>
        public void Back() 
        {
            if (_navigationJournal.CanGoBack)
            {
                _navigationJournal.GoBack();
            }            
        }
    }
}
