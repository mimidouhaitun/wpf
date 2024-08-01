using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    /// <summary>
    /// 定义一个接口，主要目的是扩展viewmodel中的通用方法，在应用启动的时候调用，打开首页
    /// </summary>
    public interface IMainWindowViewModel
    {
        void OnAppStart();
        void SetJournal(IRegionNavigationJournal journal);
    }
}
