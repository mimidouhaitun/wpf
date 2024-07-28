﻿using MyToDo.Common.Models;
using MyToDo.Service;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel: NavigationBaseViewModel
    {
        #region 字段
        private ObservableCollection<ToDoDto> toDoDtos;
        private ToDoDto currToDoDto;
        private bool isRightDrawerOpen;
        private readonly IToDoService service;
        private string search;
        private int status;
        private MyItem statusSelectedItem;
        #endregion

        #region 属性
        public DelegateCommand<string> ExecuteCommand { get; set; }
        public DelegateCommand<ToDoDto> SelectedCommand { get; set; }
        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }
        public ToDoDto CurrToDoDto
        {
            get { return currToDoDto; }
            set { currToDoDto = value; RaisePropertyChanged(); }
        }        
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                RaisePropertyChanged(); 
            }
        }
        public int Status
        {
            get => status; set
            {
                status = value;
                RaisePropertyChanged();
                GetToDoListAsync();
            }
        }
        public ObservableCollection<MyItem> StatusItems { get; set; }      
        public MyItem StatusSelectedItem
        {
            get => statusSelectedItem;
            set
            {
                statusSelectedItem = value; 
                RaisePropertyChanged();
                if (CurrToDoDto != null)
                {
                    CurrToDoDto.Status = StatusSelectedItem.Value;
                }                
            }
        }
        #endregion

        #region 方法
        public ToDoViewModel(IToDoService service, IContainerProvider containerProvider):base(containerProvider)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            this.service = service;
            StatusItems = new ObservableCollection<MyItem>()
            {
                new MyItem(){  DisplayName="待办",Value=1},
                new MyItem(){  DisplayName="已完成",Value=2},
            };
            StatusSelectedItem=StatusItems.FirstOrDefault(a=>a.Value==1);
        }
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增":
                    IsRightDrawerOpen = true; //显示右边的抽屉窗口
                    this.CurrToDoDto = new ToDoDto();
                    break;
                case "查询":
                     GetToDoListAsync();
                    break;
                case "保存":
                    Save();
                    break;
            }
            
        }

        private async void Save()
        {
           if(string.IsNullOrWhiteSpace(CurrToDoDto.Title) 
                || string.IsNullOrWhiteSpace(CurrToDoDto.Content))
            {
                return;
            }
            try
            {
                PublishLoading(true);
                if (CurrToDoDto.Id == 0)
                {
                    var result = await service.AddAsync(CurrToDoDto);
                    ToDoDtos.Add(result.Result);
                    CurrToDoDto = result.Result;                    
                }
                else
                {
                    var result = await service.UpdateAsync(CurrToDoDto);
                    if (result.Status)
                    {
                        var todo = ToDoDtos.First(a => a.Id == result.Result.Id);
                        todo.Title = result.Result.Title;
                        todo.Content = result.Result.Content;                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
               GetToDoListAsync();
                PublishLoading(false);
                IsRightDrawerOpen = false;
            }
           
        }

        private async void Selected(ToDoDto dto)
        {
            try
            {
                PublishLoading(true);
                var result = await service.GetFirstOrDefaultAsync(dto.Id);
                if (result.Status)
                {
                    CurrToDoDto = result.Result;
                    StatusSelectedItem = StatusItems.FirstOrDefault(a => a.Value == CurrToDoDto.Status);
                    IsRightDrawerOpen = true; //显示右边的抽屉窗口
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                PublishLoading(false);
            }
        }
        async void GetToDoListAsync()
        {
            base.PublishLoading(true);
            var apiResponse = await service.GetPageListAsync(new Parameters.TodoParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search=Search,
                Status=this.Status
            });
            if (apiResponse.Status)
            {
                ToDoDtos.Clear();
                foreach(var toDoDto in apiResponse.Result.Items)
                {
                    ToDoDtos.Add(toDoDto);
                }
            }
            base.PublishLoading(false);
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetToDoListAsync(); //需要放在service赋值之后
        }
        #endregion
    }
}
