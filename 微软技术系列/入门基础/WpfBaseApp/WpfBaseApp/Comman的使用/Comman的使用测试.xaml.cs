using System.Windows;

namespace WpfBaseApp.Comman的使用
{
    /// <summary>
    /// Comman的使用.xaml 的交互逻辑
    /// </summary>
    public partial class Comman的使用测试 : Window
    {
        public Comman的使用测试()
        {
            InitializeComponent();
            this.DataContext=new MainViewModel();
        }
    }
}
