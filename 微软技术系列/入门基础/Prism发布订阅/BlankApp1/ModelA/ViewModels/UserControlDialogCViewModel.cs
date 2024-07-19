using ModelA.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelA.ViewModels
{
    public class UserControlDialogCViewModel: BindableBase,IDialogAware
    {
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand SendMsgCommand { get; set; }
        /// <summary>
        /// 会自动设置到当前子窗体的标题中
        /// </summary>
        public string Title { get; private set; }
        public IEventAggregator _eventAggregator { get; }

        public event Action<IDialogResult> RequestClose;

        public UserControlDialogCViewModel(IEventAggregator eventAggregator) 
        {
            CancelCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand(Save);
            SendMsgCommand = new DelegateCommand(SendMsg);
            _eventAggregator = eventAggregator;
        }


        private void SendMsg()
        {
            _eventAggregator.GetEvent<MessageEvent>().Publish("hello");
        }

        private void Save()
        {
            OnDialogClosed();
        }

        private void Cancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

      

        public bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// 子窗体关闭时将结果返回给父窗体，RequestClose.Invoke()之后就会调用该方法，RequestClose会变为null
        /// </summary>
        public void OnDialogClosed()
        {
            if (RequestClose != null)
            {
                DialogParameters keyValuePairs = new DialogParameters();
                keyValuePairs.Add("Value", "Hello");

                DialogResult dialogResult = new DialogResult(ButtonResult.OK, keyValuePairs);

                RequestClose.Invoke(dialogResult);
            }
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            //接收父窗体传递过来的参数
            this.Title = parameters.GetValue<string>("Title");
        }
    }
}
