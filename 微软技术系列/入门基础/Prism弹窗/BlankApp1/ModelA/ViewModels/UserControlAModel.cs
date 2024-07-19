using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModelA.ViewModels
{
    /// <summary>
    /// IConfirmNavigationRequest也继承了INavigationAware接口
    /// </summary>
    public class UserControlAModel: BindableBase,INavigationAware,IConfirmNavigationRequest
    {
        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }

        /* 要让model和vie自动匹配，需要
         * 1.model的名称=view的名称+Model 
         * 2.view中加入以下两行
         * xmlns:prism="http://prismlibrary.com/"
         * prism:ViewModelLocator.AutoWireViewModel="True"
        */
        public UserControlAModel()
        { 
        
        }
        /// <summary>
        /// 是否重新创建实例
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("Title"))
            {
                this.Title = navigationContext.Parameters.GetValue<string>("Title");
            }            
        }
        /// <summary>
        /// IConfirmNavigationRequest中的接口，也继承了这个接口INavigationAware
        /// 功能：是否允许从当前模块导航到其他模块
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <param name="continuationCallback"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;
            if(MessageBox.Show("确定要切换吗？","询问",MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)
            {
                result = false;
            }
            continuationCallback(result);
        }
    }
}
