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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfBaseApp.动画
{
    /// <summary>
    /// 双精度动画.xaml 的交互逻辑
    /// </summary>
    public partial class 双精度动画 : Window
    {
        public 双精度动画()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            //animation.From = btn1.Width;
            //animation.To=btn1.Width-30;
            animation.By = -30;

            animation.Duration = TimeSpan.FromSeconds(2);
            animation.RepeatBehavior = new RepeatBehavior(3); //RepeatBehavior.Forever;
            animation.AutoReverse = true;
            btn1.BeginAnimation(Button.WidthProperty, animation);
        }
    }
}
