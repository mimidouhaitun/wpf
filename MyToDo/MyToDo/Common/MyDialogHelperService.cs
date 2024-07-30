using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyToDo.Common
{
    public interface IMyDialogHelperService:IDialogService
    {
        Task<IDialogResult> ShowDialog(string viewName, IDialogParameters parameters, string dialogHostName = "Root");
    }
    public class MyDialogHelperService : DialogService, IMyDialogHelperService
    {
        private readonly IContainerExtension containerExtension;

        public MyDialogHelperService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="parameters"></param>
        /// <param name="dialogHostName">  materialDesign:DialogHost下面的 Identifier="Root"，名称保持一致 </param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IDialogResult> ShowDialog(string viewName, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if (parameters == null)
            {
                parameters= new DialogParameters();
            }
            //获得View的实例
            var view = containerExtension.Resolve<object>(viewName);
            if (!(view is FrameworkElement))
            {
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");
            }
            var view2 = (FrameworkElement)view;
            if (view2.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view2) is null)
            {
                ViewModelLocator.SetAutoWireViewModel(view2, true);
            }
            //view对应的viewmodel需要继承自定义的接口IViewDataContent
            if (!(view2.DataContext is IViewDataContent))
            {
                throw new NullReferenceException("A dialog's ViewModel must implement the IViewDataContent interface");
            }
            IViewDataContent dataContent = (IViewDataContent)view2.DataContext;

            dataContent.DialogHostName = dialogHostName;
            DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
            {
                dataContent.OnDialogOpend(parameters);//View打开之后，回调ViewModel中的方法，目的是将参数传递给ViewModel
                eventArgs.Session.UpdateContent(view);
            };
            return (IDialogResult) await DialogHost.Show(view2, dataContent.DialogHostName, eventHandler);
        }
    }
}
