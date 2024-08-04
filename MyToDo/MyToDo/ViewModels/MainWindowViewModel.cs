using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;

namespace MyToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase, IMainWindowViewModel
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<MenuBar> menuBars;
        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;
        private IRegionNavigationJournal Journal;
        private string userName;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }
        public DelegateCommand LoginOutCommand { get; private set; }
        public string UserName
        {
            get { return userName; }
            set { userName = value;RaisePropertyChanged(); }
        }

        public MainWindowViewModel(IRegionManager regionManager,IDialogService dialogService)
        {
            menuBars=new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;
            this.dialogService = dialogService;
            GoBackCommand = new DelegateCommand(GoBack);
            GoForwardCommand = new DelegateCommand(GoForward);
            LoginOutCommand = new DelegateCommand(LoginOut);
        }

        private void LoginOut()
        {
            AppSession.UserDto = new UserDto();
            App.Current.MainWindow.Hide();
            //使用Prism自带的打开modal方法，会有标题栏，适用于与MainWindow同级别的窗口。
            //使用MyDialogHelperService不会有标题栏
            dialogService.ShowDialog("LoginView", dialogResult =>
            {
                if (dialogResult.Result == ButtonResult.OK)
                {
                    App.Current.MainWindow.Show();
                }
            });
        }

        private void GoForward()
        {
            if (Journal != null && Journal.CanGoForward)
            {
                Journal.GoForward();
            }   
        }

        private void GoBack()
        {
            if (Journal != null && Journal.CanGoBack)
            {
                Journal.GoBack();
            }
        }

        private void Navigate(MenuBar bar)
        {
            if (bar == null || string.IsNullOrWhiteSpace(bar.ViewName))
                return;
            regionManager.Regions[Extensions.PrismManager.MainViewRegionName].RequestNavigate(bar.ViewName, callback => {
                Journal=callback.Context.NavigationService.Journal;
            });
        }

        void CreateMenuBars()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", ViewName = "IndexView", Title = "首页" });
            MenuBars.Add(new MenuBar() { Icon = "NoteOutline", ViewName = "ToDoView", Title = "代办事项" });
            MenuBars.Add(new MenuBar() { Icon = "NotePlusOutline", ViewName = "MemoView", Title = "备忘录" });
            MenuBars.Add(new MenuBar() { Icon = "CogOutline", ViewName = "SettingsView", Title = "设置" });
        }

        /// <summary>
        /// 在App中调用，项目启动时调用
        /// </summary>
        public void OnAppStart()
        {
            CreateMenuBars();
            regionManager.Regions[Extensions.PrismManager.MainViewRegionName].RequestNavigate("IndexView", callback => {
                Journal = callback.Context.NavigationService.Journal;
            });

            UserName = AppSession.UserDto.UserName;
        }

        public void SetJournal(IRegionNavigationJournal journal)
        {
            this.Journal = journal;
        }
    }
}
