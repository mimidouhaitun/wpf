using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfBaseApp.Comman的使用.ViewModels
{
    public partial class PersonViewModel:ObservableObject
    {
#pragma warning disable

        private string name;

        private int age;
        private int cnt = 0;
        private string nameTemp = "";
        public RelayCommand UpdateNameCommand { get; } //用于在页面中绑定Comman
        public RelayCommand UpdateAgeCommand { get; }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string NameTemp
        {
            get => nameTemp;
            set => SetProperty(ref nameTemp, value);
        }
        public int Cnt
        {
            get=>cnt;
            set => SetProperty(ref cnt, value);
        }
        public int Age
        {
            get => age;
            set => SetProperty(ref age, value);
        }

        public void UpdateName()
        {
            name= nameTemp+cnt;
            OnPropertyChanged("Name");
            cnt = cnt + 1;
            OnPropertyChanged("Cnt");
        }
        public void UpdateAge()
        {
            age += cnt;
            OnPropertyChanged("Age");
            cnt = cnt + 1;
            OnPropertyChanged("Cnt");
        }

        public PersonViewModel()
        {
            this.nameTemp=this.Name = "John Doe";
            this.Age = 30;
            this.UpdateAgeCommand = new RelayCommand(UpdateAge);
            this.UpdateNameCommand=new RelayCommand(UpdateName);
        }
    }
}
