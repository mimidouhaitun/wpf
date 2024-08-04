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
using System.Xml.Linq;

namespace MyToDo.Common
{
    public interface IMyDialogHelperService:IDialogService
    {
        Task<IDialogResult> ShowDialogAsync(string viewName, IDialogParameters parameters, string dialogHostName = "Root");
    }
    public class MyDialogHelperService : DialogService, IMyDialogHelperService
    {
        private readonly IContainerExtension containerExtension;

        public MyDialogHelperService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        /// <summary>
        /// 使用materialDesignThemes打开dialog，并强制dialogView的viewModel需要继承自定义的接口
        /// 使用这种方式，不会有标题栏。MsgView.xaml（删除数据时弹出是否确认要删除）、
        /// AddMemoView.xaml、AddToDoView.xaml（首页添加待办事项和备忘录）
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="parameters"></param>
        /// <param name="dialogHostName">  materialDesign:DialogHost下面的 Identifier="Root"，名称保持一致 </param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IDialogResult> ShowDialogAsync(string viewName, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if (parameters == null)
                parameters = new DialogParameters();

            //从容器当中去除弹出窗口的实例
            var content = containerExtension.Resolve<object>(viewName);

            //验证实例的有效性 
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");
            //自动注入viewModel
            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);
            //dialog的viewModel继承自定义的接口，以便扩展自定义方法
            if (!(dialogContent.DataContext is IViewDataContent viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            viewModel.DialogHostName = dialogHostName; //materialDesign:DialogHost下面的 Identifier="Root"，名称保持一致

            DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
            {
                if (viewModel is IViewDataContent aware)
                {
                    aware.OnDialogOpend(parameters);
                }
                eventArgs.Session.UpdateContent(content);
            };
            if (DialogHost.IsDialogOpen(viewModel.DialogHostName))
            {
                DialogHost.Close(viewModel.DialogHostName);
            }
            return (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);        
        }
    }
}
