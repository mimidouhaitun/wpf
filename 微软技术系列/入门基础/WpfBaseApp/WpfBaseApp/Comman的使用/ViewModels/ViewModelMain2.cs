using System.Windows;

namespace WpfBaseApp.Comman的使用.ViewModels
{
    /// <summary>
    /// 简化了MainViewModel的写法
    /// </summary>
    public class ViewModelMain2 : ViewModelBase
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
                OnPropertyChanged();
            }
        }
        public MyCommand ShowMsg { get; set; }
        public ViewModelMain2()
        {
            ShowMsg = new MyCommand(Show);
            Name = "";
        }

        public void Show()
        {
            Name = "张三";
            MessageBox.Show("点击了按钮");
        }

    }
}
