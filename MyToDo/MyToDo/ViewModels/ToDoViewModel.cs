using MyToDo.Common.Models;
using MyToDo.Service;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel:BindableBase
    {
        private ObservableCollection<ToDoDto> toDoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        private bool isRightDrawerOpen;
        private readonly IToDoService service;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        public ToDoViewModel(IToDoService service)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            AddCommand = new DelegateCommand(Add);
            this.service = service;
            CreateToDoListAsync(); //需要放在service赋值之后
        }

        private void Add()
        {
            IsRightDrawerOpen = true;
        }
        public DelegateCommand AddCommand { get; set; }
        async void CreateToDoListAsync()
        {
            var apiResponse = await service.GetPageListAsync(new Parameters.QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100
            });
            if (apiResponse.Status)
            {
                ToDoDtos.Clear();
                foreach(var toDoDto in apiResponse.Result.Items)
                {
                    ToDoDtos.Add(toDoDto);
                }
            }
        }
    }
}
