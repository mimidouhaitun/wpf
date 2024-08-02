using MyToDo.Extensions;
using Prism.Events;
using System.Windows.Controls;

namespace MyToDo.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        public LoginView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.eventAggregator = eventAggregator;

            eventAggregator.SubscribeStr((msg) =>
            {
                mySnackBar.MessageQueue.Enqueue(msg);
            }, "LoginView");
        }
    }
}
