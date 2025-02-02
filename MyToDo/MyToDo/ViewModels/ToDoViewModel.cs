﻿using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Service;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel: NavigationBaseViewModel
    {
        #region 字段
        private ObservableCollection<ToDoDto> toDoDtos;
        private ToDoDto currToDoDto;
        private bool isRightDrawerOpen;
        private readonly IToDoService service;
        private readonly IMyDialogHelperService myDialog;
        private string search;
        private int status;
        private MyItem statusSelectedItem;
        #endregion

        #region 属性
        public DelegateCommand<string> ExecuteCommand { get; set; }
        public DelegateCommand<ToDoDto> SelectedCommand { get; set; }
        public DelegateCommand<ToDoDto> DeleteCommand { get; set; }
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
            }
        }
        #endregion

        #region 方法
        public ToDoViewModel(IToDoService service, IContainerProvider containerProvider,IMyDialogHelperService myDialog):base(containerProvider)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            DeleteCommand = new DelegateCommand<ToDoDto>(DeleteAsync);
            this.service = service;
            this.myDialog = myDialog;
            StatusItems = new ObservableCollection<MyItem>()
            {
                new MyItem(){  DisplayName="待办",Value=1},
                new MyItem(){  DisplayName="已完成",Value=2},
            };
            StatusSelectedItem=StatusItems.FirstOrDefault(a=>a.Value==1);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        private async void DeleteAsync(ToDoDto dto)
        {
            try
            {
                PublishLoading(true);
                IDialogParameters parameters = new DialogParameters();
                parameters.Add("Title", "询问");
                parameters.Add("Content", "确定要删除待办事项吗？");
                var dialogResult = await myDialog.ShowDialogAsync("MsgView", parameters, "Root");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK)
                    return;
                var result = await service.DeleteAsync(dto.Id);
                if (result.Status)
                {
                    var d = ToDoDtos.First(d => d.Id == dto.Id);
                    ToDoDtos.Remove(d);
                }
            }
            finally
            {
                PublishLoading(false);
            }
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
                CurrToDoDto.Status = (StatusEnum)StatusSelectedItem.Value;
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
                    StatusSelectedItem = StatusItems.FirstOrDefault(a => a.Value == (int)CurrToDoDto.Status);
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
            if (navigationContext.Parameters.ContainsKey("Status"))
            {
                this.Status = navigationContext.Parameters.GetValue<int>("Status");
            }            
            GetToDoListAsync(); //需要放在service赋值之后
        }
        #endregion
    }
}
