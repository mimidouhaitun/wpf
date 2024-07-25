using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyToDo.Api.Dtos
{
    public class BaseDto:INotifyPropertyChanged
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
