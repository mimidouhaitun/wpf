using ModelA.ViewModels;
using ModelA.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelA
{
    public class ModelAProfile : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //暴露出去
            //containerRegistry.RegisterForNavigation<UserControlA>();
            containerRegistry.RegisterForNavigation<UserControlA, UserControlAModel>();
            //自动寻找名称为：view控件名+ViewModel.cs命名的model，省略了手动绑定
            containerRegistry.RegisterDialog<UserControlDialogC>(); 
        }
    }
}
