using MyToDo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddMemoViewModel:BindableBase,IDialogAware
    {

        #region 字段

        private MemoDto model;

        public event Action<IDialogResult> RequestClose;

        #endregion

        #region 属性
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public MemoDto Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }

        public string Title => "添加待办事项";
        #endregion

        #region 方法
        public AddMemoViewModel() {
            CancelCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand(Save);
        }

        private void Cancel()
        {
           
        }

        private void Save()
        {
          
        }

        public bool CanCloseDialog()
        {
            return true;
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
