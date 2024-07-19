using DryIoc;
using ModelA.Event;
using Prism.Events;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModelA.Views
{
    /// <summary>
    /// UserControlC.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlDialogC : UserControl
    {
        public UserControlDialogC(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            EventAggregator = eventAggregator;
            EventAggregator.GetEvent<MessageEvent>().Subscribe(SubscribeMsg);
        }

        public IEventAggregator EventAggregator { get; }

        public void SubscribeMsg(string msg)
        {
            MessageBox.Show("接收到消息：" + msg);
        }
        /// <summary>
        /// 取消订阅按钮的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnsubscribe_Click(object sender, RoutedEventArgs e)
        {
            EventAggregator.GetEvent<MessageEvent>().Unsubscribe(SubscribeMsg);
            MessageBox.Show("取消订阅成功");
        }
    }
}
