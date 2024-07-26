using MyToDo.Extensions;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    /// <summary>
    /// 当作为导航窗口打开的时候，调用这里的方法
    /// </summary>
    public class NavigationBaseViewModel :BindableBase, INavigationAware
    {
        private readonly IContainerProvider containerProvider; //依赖注入的容器
        private readonly IEventAggregator eventAggregator;
        public NavigationBaseViewModel(IContainerProvider containerProvider) 
        {
            this.containerProvider = containerProvider;
            eventAggregator=containerProvider.Resolve<IEventAggregator>(); //从容器中获取实例
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        /// <summary>
        /// 该类作为基类使用，该方法是接口中的方法，改成虚方法，继承类可以重写该方法
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
        /// <summary>
        /// 给父类调用
        /// </summary>
        /// <param name="IsOpen"></param>
        public void PublishLoading(bool IsOpen)
        {
            eventAggregator.PublishExt(new Common.Events.UpdateModel() { IsOpen = IsOpen });
        }
    }
}
