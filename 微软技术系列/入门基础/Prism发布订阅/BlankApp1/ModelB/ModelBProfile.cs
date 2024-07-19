using ModelB.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;

namespace ModelB
{
    public class ModelBProfile : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
         
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UserControlB>();
        }
    }
}
