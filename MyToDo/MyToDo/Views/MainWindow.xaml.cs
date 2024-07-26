using MyToDo.Extensions;
using Prism.Events;
using System.Windows;

namespace MyToDo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.btnMin.Click += (s, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };

            this.btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }
            };

            this.btnClose.Click += (s, e) =>
            {
                this.Close();
            };
            //鼠标按下拖动，移动窗口
            this.colorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
            //鼠标双击最大化或者还原
            this.colorZone.MouseDoubleClick += (s, e) => {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }
            };

            eventAggregator.SubscribeExt(arg =>
            {
                dialogHostRoot.IsOpen=arg.IsOpen; //设置打开或者关闭对话框
                if (dialogHostRoot.IsOpen)
                {
                    dialogHostRoot.DialogContent = new ProgressView();//设置对话框内容
                }
            });
        }

        private void menuBar_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            drawerHost.IsLeftDrawerOpen = false;
        }
    }
}
