namespace MyToDo.Api.Dtos
{
    public class UserDto:BaseDto
    {
#pragma warning disable
        private string account;
        private string userName;
        private string password;
        public string Account
        {
            get => account; 
            set
            {
                account = value;
                OnPropertyChanged();
            }
        }
        public string? UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        public string PassWord
        {
            get => password; 
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
    }
}
