﻿using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddMemoViewModel : BindableBase, IViewDataContent
    {
        private MemoDto currDto;

        #region 属性
        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
       
        public MemoDto CurrDto
        {
            get { return currDto; }
            set { currDto = value; }
        }
        #endregion

        #region 方法
        public AddMemoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
            }
        }
        private void Save()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                var para = new DialogParameters();
                para.Add("memoDto", CurrDto);
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, para));
            }
        }
        /// <summary>
        /// 以dialog方式打开之后，回调的方法，目的是获取dialog传递过来的参数
        /// </summary>
        /// <param name="parameters"></param>
        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("memoDto"))
            {
                var dto = parameters.GetValue<MemoDto>("memoDto");
                CurrDto = JsonConvert.DeserializeObject<MemoDto>(JsonConvert.SerializeObject(dto));               
            }
            else
            {
                CurrDto = new MemoDto();
            }
        }
        #endregion

    }
}
