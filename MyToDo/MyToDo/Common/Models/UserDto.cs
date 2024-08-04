using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    public class UserDto:BindableBase
    {
        private int id;
        private string account;
        private string userName;
        private string passWord;
        private string confirmPassWord;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Account
        {
            get => account; 
            set { account = value; RaisePropertyChanged(); }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        public string PassWord
        {
            get => passWord; 
            set { passWord = value; RaisePropertyChanged(); }
        }
        public string ConfirmPassWord
        {
            get => confirmPassWord; 
            set { confirmPassWord = value; RaisePropertyChanged(); }
        }
    }
}
