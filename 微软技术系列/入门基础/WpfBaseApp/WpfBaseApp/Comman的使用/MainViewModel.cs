using System.Windows;

namespace WpfBaseApp.Comman的使用
{
    /// <summary>
    /// 简化了MainViewModel的写法
    /// </summary>
    public class MainViewModel2 : ViewModelBase
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
        public MainViewModel2() { 
            this.ShowMsg = new MyCommand(Show);
            this.Name = "";
        }

        public void Show()
        {
            this.Name = "张三";
            MessageBox.Show("点击了按钮");
        }

    }
}
