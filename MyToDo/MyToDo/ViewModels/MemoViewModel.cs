using MyToDo.Common.Models;
using MyToDo.Service;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows;

namespace MyToDo.ViewModels
{
    public class MemoViewModel : NavigationBaseViewModel
    {
        #region 字段
        private ObservableCollection<MemoDto> memoDtos;
        private bool isRightDrawerOpen;
        private readonly IMemoService service;
        private MemoDto currentDto;
        private string search;
        public Visibility isEmptyList;
        #endregion

        #region 属性
        public DelegateCommand<MemoDto> SelectedCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ExecuteCommand { get; set; }
        public DelegateCommand<MemoDto> DeleteCommand { get; set; }
        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { 
                memoDtos = value;
                RaisePropertyChanged();
            }
        }
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }
        public DelegateCommand AddCommand { get; set; }
        public MemoDto CurrentDto
        {
            get => currentDto;
            set
            {
                currentDto = value;
                RaisePropertyChanged();
            }
        }
        public string Search
        {
            get => search; 
            set
            {
                search = value;
                RaisePropertyChanged();
            }
        }
        public Visibility IsEmptyList
        {
            get => isEmptyList; 
            set
            {
                isEmptyList = value; RaisePropertyChanged();
            }
        }
        #endregion

        #region 方法
        public MemoViewModel(IMemoService service,IContainerProvider provider):base(provider)
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            AddCommand = new DelegateCommand(Add);
            SelectedCommand = new DelegateCommand<MemoDto>(Selected);
            SaveCommand = new DelegateCommand(Save);
            ExecuteCommand = new DelegateCommand(Execute);
            DeleteCommand = new DelegateCommand<MemoDto>(Delete);
            this.service = service;
        }

        private async void Delete(MemoDto dto)
        {
            try
            {
                PublishLoading(true);
                var result=await service.DeleteAsync(dto.Id);
                if (result.Status)
                {
                    var d = MemoDtos.First(d => d.Id == dto.Id);
                    MemoDtos.Remove(d);
                    IsEmptyList = MemoDtos.Count == 0 ? Visibility.Visible : Visibility.Hidden;
                }
            }
            finally
            {
                PublishLoading(false);
            }
            
        }

        private async void Execute()
        {
            try
            {
                PublishLoading(true);
                var result = await service.GetPageListAsync(new Parameters.QueryParameter()
                {
                    PageIndex = 0,
                    PageSize = 100,
                    Search = this.Search,
                });
                if (result.Status)
                {
                    MemoDtos.Clear();
                    foreach (var memoDto in result.Result.Items)
                    {
                        MemoDtos.Add(memoDto);
                    }
                    IsEmptyList = MemoDtos.Count == 0 ? Visibility.Visible : Visibility.Hidden;
                }
            }
            finally
            {
                PublishLoading(false);
            }
        }

        private async void Save()
        {
            try
            {
                PublishLoading(true);
                if (string.IsNullOrWhiteSpace(CurrentDto.Title) || string.IsNullOrWhiteSpace(CurrentDto.Content))
                    return;
                if (CurrentDto.Id > 0)
                {
                   var result= await service.UpdateAsync(CurrentDto);
                    if (result.Status)
                    {
                        var dto=MemoDtos.First(a=>a.Id==CurrentDto.Id);
                        dto.Title = CurrentDto.Title;
                        dto.Content = CurrentDto.Content;
                    }
                }                    
                else
                {
                   var result= await service.AddAsync(CurrentDto);
                    if (result.Status)
                    {
                        MemoDtos.Add(result.Result);
                    }
                }                    
            }
            finally
            {
                PublishLoading(false);
                IsRightDrawerOpen = false;
                IsEmptyList = MemoDtos.Count == 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private async void Selected(MemoDto dto)
        {
            IsRightDrawerOpen = true;
            try
            {
                PublishLoading(true);
                var result = await service.GetFirstOrDefaultAsync(dto.Id);
                if (result.Status)
                {
                    CurrentDto = result.Result;
                }
            }
            finally
            {
                PublishLoading(false);
            }           
        }

        private void Add()
        {
            IsRightDrawerOpen = true;
            CurrentDto=new MemoDto();
        }
        async void GetToDoListAsync()
        {
            try
            {
                PublishLoading(true);
                var result = await service.GetPageListAsync(new Parameters.QueryParameter()
                {
                    PageIndex = 0,
                    PageSize = 100
                });
                if (result.Status)
                {
                    MemoDtos.Clear();
                    foreach (var memoDto in result.Result.Items)
                    {
                        MemoDtos.Add(memoDto);
                    }
                    IsEmptyList = MemoDtos.Count == 0 ? Visibility.Visible : Visibility.Hidden;
                }
            }
            finally
            {
                PublishLoading(false);
            }            
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetToDoListAsync();          
        }
        #endregion

    }
}
