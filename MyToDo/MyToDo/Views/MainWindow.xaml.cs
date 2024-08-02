using MyToDo.Common;
using MyToDo.Extensions;
using Prism.Events;
using Prism.Services.Dialogs;
using System.Windows;

namespace MyToDo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMyDialogHelperService myDialog;

        public MainWindow(IEventAggregator eventAggregator,IMyDialogHelperService myDialog)
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

            this.btnClose.Click += async (s, e) =>
            {
                IDialogParameters parameters = new DialogParameters();
                parameters.Add("Title", "询问");
                parameters.Add("Content", "确定要退出系统吗？");
                var dialogResult = await myDialog.ShowDialogAsync("MsgView", parameters, "Root");
                if (dialogResult.Result == Prism.Services.Dialogs.ButtonResult.No)
                    return;
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

            //订阅提示消息
            eventAggregator.SubscribeStr((arg) =>
            {
                mySnackBar.MessageQueue.Enqueue(arg);
            }, "ToMainView");

            this.myDialog = myDialog;
        }

        private void menuBar_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            drawerHost.IsLeftDrawerOpen = false;
        }
    }
}
