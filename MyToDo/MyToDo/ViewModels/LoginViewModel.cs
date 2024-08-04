using Microsoft.Win32;
using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using MyToDo.Service;
using Prism.Commands;
using Prism.Events;
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
        private readonly ILoginService loginService;
        private readonly IEventAggregator eventAggregator;
        public event Action<IDialogResult> RequestClose;
        private int selectIndex;
        #endregion

        #region 属性
        private UserDto userDto;

        public UserDto UserDto
        {
            get { return userDto; }
            set { userDto = value;RaisePropertyChanged(); }
        }

        public bool CanCloseDialog()
        {
            return true;
        }
        public string Title =>"系统登录";
        public DelegateCommand<string> ExecuteCommand { get; set; }        
        public int SelectIndex
        {
            get { return selectIndex; }
            set { selectIndex = value; RaisePropertyChanged(); }
        }

        #endregion

        #region 方法
        public LoginViewModel(ILoginService loginService,IEventAggregator eventAggregator)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            UserDto = new UserDto();
            this.loginService = loginService;
            this.eventAggregator = eventAggregator;
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login":
                    Login();
                    break;
                case "GoToRegister":
                    GoToRegister();
                    break;
                case "SaveRegister":
                    SaveRegister();
                    break;
                case "BackToLogin":
                    BackToLogin();
                    break;
            }
        }

        private void GoToRegister()
        {
            UserDto = new UserDto();
            SelectIndex = 1;
        }

        private void BackToLogin()
        {
           SelectIndex = 0;
        }

        private async void SaveRegister()
        {
            if (UserDto==null || string.IsNullOrWhiteSpace(UserDto.PassWord) || string.IsNullOrEmpty(UserDto.Account))
            {
                eventAggregator.PublishStr(new Common.Events.MessageModel()
                {
                    Message = "用户名和密码不允许为空",
                    FilterName = "LoginView"
                });
                return;
            }
            if (string.IsNullOrWhiteSpace( UserDto.PassWord) ||UserDto.PassWord != UserDto.ConfirmPassWord)
            {
                eventAggregator.PublishStr(new Common.Events.MessageModel() { Message = "两次输入的密码不一致", FilterName = "ToMainView" });
                return;
            }
            var response = await loginService.Register(UserDto);
            if (response.Status)
            {
                eventAggregator.PublishStr(new Common.Events.MessageModel() { Message = "注册成功", FilterName = "ToMainView" });
               SelectIndex = 1;
            }
            else
            {
                eventAggregator.PublishStr(new Common.Events.MessageModel() { Message = "注册失败" + response.Message, FilterName = "LoginView" });                
            }
        }

        private async void Login()
        {
            var response = await loginService.LoginAsync(UserDto);
            if (response.Status)
            {
                AppSession.UserDto = response.Result;
                eventAggregator.PublishStr(new Common.Events.MessageModel()
                {
                    Message = "登录成功",
                    FilterName = "ToMainView"
                });
                RequestClose.Invoke(new DialogResult(ButtonResult.OK));
            }
            else
            {
                eventAggregator.PublishStr(new Common.Events.MessageModel()
                {
                    Message = "登录失败" + response.Message,
                    FilterName = "LoginView"
                });
            }
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
