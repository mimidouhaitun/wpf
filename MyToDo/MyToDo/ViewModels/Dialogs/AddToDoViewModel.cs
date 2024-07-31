using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddToDoViewModel : BindableBase,IViewDataContent
    {
        private StatusItem statusSelectedItem;

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public ObservableCollection<StatusItem> StatusItems { get; set; }  
        public StatusItem StatusSelectedItem
        {
            get { return  statusSelectedItem; }
            set {  statusSelectedItem = value;RaisePropertyChanged(); }
        }        
        private ToDoDto currDto;

        public ToDoDto CurrDto
        {
            get { return currDto; }
            set { currDto = value; RaisePropertyChanged(); }
        }

        public AddToDoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
            StatusItems = new ObservableCollection<StatusItem>()
            {
                 new StatusItem(){ DisplayName="未完成",Value=StatusEnum.未完成 },
                 new StatusItem(){DisplayName="已完成",Value=StatusEnum.已完成},
            };
        }

        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
            }
        }

        private void Save()
        {           
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                var para = new DialogParameters();
                para.Add("todoDto", CurrDto);
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK,para));
            }
        }

        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("todoDto"))
            {
                var dto = parameters.GetValue<ToDoDto>("todoDto");
                CurrDto = JsonConvert.DeserializeObject<ToDoDto>(JsonConvert.SerializeObject(dto));
                StatusSelectedItem = StatusItems.First(a => a.Value == dto.Status);
            }
            else
            {
                CurrDto = new ToDoDto() { Status = StatusEnum.未完成 };
            }
        }
    }
}
