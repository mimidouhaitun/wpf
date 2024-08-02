using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class LoginViewModel:BindableBase,IDialogAware
    {
        #region 字段
        private string userName;
        private string passWord;
        public event Action<IDialogResult> RequestClose;
        #endregion

        #region 属性
        public string UserName
        {
            get { return userName; }
            set { userName = value;RaisePropertyChanged(); }
        }

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }
        public bool CanCloseDialog()
        {
            return true;
        }
        public string Title =>"系统登录";
        #endregion

        #region 方法
        public LoginViewModel() { 
        
        }              

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           
        }
        #endregion
    }
}
