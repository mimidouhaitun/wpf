using MyToDo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class SettingsViewModel:BindableBase
    {
        private ObservableCollection<MenuBar> menuBars;
        private readonly IRegionManager regionManager;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public SettingsViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            this.regionManager = regionManager;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            CreateMenuBars();
        }

        private void Navigate(MenuBar bar)
        {
            if (bar == null || string.IsNullOrWhiteSpace(bar.ViewName))
                return;
            regionManager.Regions[Extensions.PrismManager.SettingsViewRegionName].RequestNavigate(bar.ViewName);
        }

        void CreateMenuBars()
        {
            MenuBars.Add(new MenuBar() { Icon = "Palette", ViewName = "SkinView", Title = "个性化" });
            MenuBars.Add(new MenuBar() { Icon = "Cogs", ViewName = "ToDoView", Title = "系统设置" });
            MenuBars.Add(new MenuBar() { Icon = "InformationSlabCircleOutline", ViewName = "AboutView", Title = "关于更多" });
        }
    }
}
