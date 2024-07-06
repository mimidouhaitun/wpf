using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfBaseApp.Comman的使用
{
    public class MainViewModel:INotifyPropertyChanged
    {
#pragma warning disable
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public MyCommand ShowMsg { get; set; }
        public MainViewModel() { 
            this.ShowMsg = new MyCommand(Show);
            this.Name = "";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Show()
        {
            this.Name = "张三";
            MessageBox.Show("点击了按钮");
        }

    }
}
