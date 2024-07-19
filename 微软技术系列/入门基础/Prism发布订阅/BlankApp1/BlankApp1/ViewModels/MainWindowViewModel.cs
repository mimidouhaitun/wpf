using BlankApp1.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;

namespace BlankApp1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService dialogService;
        private IRegionNavigationJournal _navigationJournal;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public DelegateCommand<string> OpenCommand { get; private set; }
        public DelegateCommand GoBack { get; private set; }
        public DelegateCommand OpenDialog { get; private set; }
        public DelegateCommand<string> OpenDialog2 { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager,IDialogService dialogService)
        {
            _regionManager = regionManager;
            this.dialogService = dialogService;
            OpenCommand = new DelegateCommand<string>(Open);
            GoBack = new DelegateCommand(Back);
            OpenDialog = new DelegateCommand(OpenDialogFunc);
            OpenDialog2 = new DelegateCommand<string>(OpenDialogFunc2);
        }

        private void OpenDialogFunc2(string objName)
        {
            //父窗体打开子窗体，并向子窗体传递参数，会传递回调函数
            DialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("Title", "测试弹窗");
            dialogService.ShowDialog(objName,dialogParameters, callBack => {
                //回调方法，获得子窗体返回的结果
                if (callBack.Result == ButtonResult.OK)
                {
                    string result=callBack.Parameters.GetValue<string>("Value");
                }
            });
        }

        private void OpenDialogFunc()
        {
            var viewc = new ViewC();
            viewc.ShowDialog();
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
