using MaterialDesignThemes.Wpf;
using MyToDo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;

namespace MyToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<MenuBar> menuBars;
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal Journal;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            menuBars=new ObservableCollection<MenuBar>();
            CreateMenuBars();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;
            GoBackCommand = new DelegateCommand(GoBack);
            GoForwardCommand = new DelegateCommand(GoForward);
        }

        private void GoForward()
        {
            if (Journal!=null && Journal.CanGoBack)
            {
                Journal.GoBack();
            }   
        }

        private void GoBack()
        {
            if (Journal!=null && Journal.CanGoForward)
            {
                Journal.GoForward();
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
    }
}
