using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfBaseApp.Comman的使用.ViewModels;

namespace WpfBaseApp.Comman的使用
{
    /// <summary>
    /// Comman_MVVMlight.xaml 的交互逻辑
    /// </summary>
    public partial class Comman_MVVMlight : Window
    {
        public Comman_MVVMlight()
        {
            InitializeComponent();
            this.DataContext = new PersonViewModel();
        }
    }
}
