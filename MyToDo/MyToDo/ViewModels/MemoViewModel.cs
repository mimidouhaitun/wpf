using MyToDo.Common.Models;
using MyToDo.Service;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class MemoViewModel : BindableBase
    {

        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

        private bool isRightDrawerOpen;
        private readonly IMemoService service;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }
        public DelegateCommand AddCommand { get; set; }

        public MemoViewModel(IMemoService service)
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            AddCommand = new DelegateCommand(Add);
            this.service = service;
            CreateToDoList();
        }

        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        async void CreateToDoList()
        {
            var result = await service.GetPageListAsync(new Parameters.QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100
            });
            if (result.Status)
            {
                foreach(var memoDto in result.Result.Items)
                {
                    MemoDtos.Add(memoDto);
                }
            }
        }
    }
}
