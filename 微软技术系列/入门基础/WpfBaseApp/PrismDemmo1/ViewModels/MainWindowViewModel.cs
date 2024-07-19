using Prism.Commands;
using Prism.Mvvm;
using PrismDemmo1.Views;

namespace PrismDemmo1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand<string> OpenCommand { get; private set; }
        public MainWindowViewModel()
        {
            OpenCommand = new DelegateCommand<string>(Open);
        }

        private object body;
        public object Body
        {
            get { return body; }
            set
            {
                body = value;
                RaisePropertyChanged();
            }
        }
        private void Open(string obj)
        {
            switch (obj)
            {
                case "ViewA":
                    Body = new UserControlA();
                    break;
                case "ViewB":
                    Body = new UserControlB();
                    break;
                case "ViewC":
                    Body = new UserControlC();
                    break;
            }
        }
    }
}
