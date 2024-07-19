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
            //containerRegistry.RegisterForNavigation<UserControlA>();
            containerRegistry.RegisterForNavigation<UserControlA, UserControlAModel>();
        }
    }
}
