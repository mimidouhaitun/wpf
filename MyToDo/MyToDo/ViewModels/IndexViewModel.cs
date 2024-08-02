using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using MyToDo.Parameters;
using MyToDo.Service;
using MyToDo.Views.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class IndexViewModel:NavigationBaseViewModel
    {
        #region 字段
        private ObservableCollection<TaskBar> taskBars;
        private ObservableCollection<ToDoDto> toDoDtos;
        private ObservableCollection<MemoDto> memoDtos;
        private readonly IMyDialogHelperService myDialogHelper;
        private readonly IToDoService toDoService;
        private readonly IMemoService memoService;
        private readonly IMyDialogHelperService myDialog;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private string title;
        #endregion

        #region 属性
        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }
        public DelegateCommand<string> ExecuteCommand { get; set; }
        public DelegateCommand<ToDoDto> ToggleStatusCommand { get; set; }
        public DelegateCommand<ToDoDto> EditTodoCommand { get; set; }
        public DelegateCommand<MemoDto> EditMemoCommand { get; set; }
        public DelegateCommand<TaskBar> CardClickCommand { get; set; }
        public ToDoSummaryDto ToDoSummaryDto { get; set; }
        public string Title
        {
            get => title; 
            set
            {
                title = value; RaisePropertyChanged();
            }
        }
        #endregion 属性

        #region 方法
        public IndexViewModel(IMyDialogHelperService myDialogHelper,IToDoService toDoService,
            IMemoService memoService,IContainerProvider provider,IMyDialogHelperService myDialog,
            IRegionManager regionManager, IEventAggregator eventAggregator) :base(provider) 
        {
            TaskBars = new ObservableCollection<TaskBar>();
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            ToggleStatusCommand = new DelegateCommand<ToDoDto>(ToggleStatus);
            EditTodoCommand = new DelegateCommand<ToDoDto>(EditTodo);
            EditMemoCommand = new DelegateCommand<MemoDto>(EditMemo);
            CardClickCommand = new DelegateCommand<TaskBar>(CardClick);
            this.myDialogHelper = myDialogHelper;
            this.toDoService = toDoService;
            this.memoService = memoService;
            this.myDialog = myDialog;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            Title = $"你好，痕迹！今天是{DateTime.Now.GetDateTimeFormats('D')[1].ToString()}";
        }

        private void CardClick(TaskBar bar)
        {
            if (bar == null || string.IsNullOrWhiteSpace(bar.Target))
                return;
            var para = new NavigationParameters();
            if(bar.Title == "已完成")
            {
                para.Add("Status", 2);
            }else 
            {
                para.Add("Status", 0);
            }
            
            regionManager.Regions[Extensions.PrismManager.MainViewRegionName].RequestNavigate(bar.Target, callback => {
                var viewModel = App.Current.MainWindow.DataContext as IMainWindowViewModel;
                viewModel.SetJournal(callback.Context.NavigationService.Journal);
            },para);
        }
        /// <summary>
        /// 设置完成或者未完成
        /// </summary>
        /// <param name="dto"></param>
        private async void ToggleStatus(ToDoDto dto)
        {
            IDialogParameters parameters = new DialogParameters();
            parameters.Add("Title", "询问");
            parameters.Add("Content", "确定要完成待办事项吗？");
            var result1=await myDialog.ShowDialogAsync("MsgView", parameters, "Root");
            if (result1.Result == ButtonResult.No)
            {
                var d = ToDoDtos.First(a => a.Id == dto.Id);
                d.Status=StatusEnum.未完成;
                return;
            }

            var result = await toDoService.UpdateAsync(dto);
            if (result.Status)
            {
                var dto2=ToDoDtos.First(a => a.Id == dto.Id);
                ToDoDtos.Remove(dto2);
            }
            GetSummary();

            eventAggregator.PublishStr(new Common.Events.MessageModel() { Message = "已完成", FilterName = "ToMainView" });
        }

        private void Execute(string obj)
        {
            if (obj == "新增备忘录")
            {
                AddMemoAsync();
            }else if (obj == "新增待办")
            {
                AddToDoAsync();
            }
        }

        private async void AddToDoAsync()
        {
            //Root,和MainWindow.xaml中的<materialDesign:DialogHost下的Identifier="Root"属性保持一致
           var result=await myDialogHelper.ShowDialogAsync("AddToDoView",null,"Root");
            if (result.Result == ButtonResult.OK)
            {
                var toDoDto = result.Parameters.GetValue<ToDoDto>("todoDto");
                if (toDoDto.Id > 0)
                {
                    var result2=await toDoService.UpdateAsync(toDoDto);
                    if (result2.Status)
                    {
                        var todo = ToDoDtos.First(a => a.Id == toDoDto.Id);
                        todo.Title = toDoDto.Title;
                        todo.Content=toDoDto.Content;
                        todo.Status= toDoDto.Status;
                    }
                }
                else
                {
                    var result2=await toDoService.AddAsync(toDoDto);
                    if (result2.Status)
                    {
                        ToDoDtos.Insert(0,toDoDto);
                    }
                }
                GetSummary();
            }
        }

        private async void AddMemoAsync()
        {
            var result = await myDialogHelper.ShowDialogAsync("AddMemoView", null, "Root");
            if (result.Result == ButtonResult.OK)
            {
                var memoDto = result.Parameters.GetValue<MemoDto>("memoDto");
                if (memoDto.Id > 0)
                {
                    var result2 = await memoService.UpdateAsync(memoDto);
                    if (result2.Status)
                    {
                        var todo = MemoDtos.First(a => a.Id == memoDto.Id);
                        todo.Title = memoDto.Title;
                        todo.Content = memoDto.Content;
                    }
                }
                else
                {
                    var result2 = await memoService.AddAsync(memoDto);
                    if (result2.Status)
                    {
                        MemoDtos.Insert(0, memoDto);
                    }
                }
                GetSummary();
            }
        }

        private async void EditMemo(MemoDto dto)
        {
            IDialogParameters parameters = new DialogParameters();
            parameters.Add("memoDto", dto);
            var result = await myDialogHelper.ShowDialogAsync("AddMemoView", parameters, "Root");
            if (result.Result == ButtonResult.OK)
            {
                var d = result.Parameters.GetValue<MemoDto>("memoDto");
                var result2 = await memoService.UpdateAsync(d);
                if (result2.Status)
                {
                    var t = MemoDtos.First(a => a.Id == d.Id);
                    t.Title = d.Title;
                    t.Content = d.Content;
                }
            }
        }

        private async void EditTodo(ToDoDto dto)
        {
            IDialogParameters parameters = new DialogParameters();
            parameters.Add("todoDto", dto);
            var result = await myDialogHelper.ShowDialogAsync("AddToDoView", parameters, "Root");
            if (result.Result == ButtonResult.OK)
            {
                var d = result.Parameters.GetValue<ToDoDto>("todoDto");
                var result2 = await toDoService.UpdateAsync(d);
                if (result2.Status)
                {
                    var t = ToDoDtos.First(a => a.Id == d.Id);
                    t.Title = d.Title;
                    t.Content = d.Content;
                    t.Status = d.Status;
                }
            }
        }

        void CreateTaskBars()
        {
            TaskBars.Clear();
            TaskBars.Add(new TaskBar() { Icom = "ClockFast", Title = "汇总", Content ="0", Color = "#FF0CA0FF", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icom = "ClockCheckOutline", Title = "已完成", Content = "", Color = "#FF1ECA3A", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icom = "ChartLineVariant", Title = "完成比例", Content = "", Color = "#FF02C6DC", Target = "" });
            TaskBars.Add(new TaskBar() { Icom = "PlaylistStar", Title = "备忘录", Content = "", Color = "#FFFFA000", Target = "MemoView" });   
        }

        private async void GetSummary()
        {
            var toDoSummary = await toDoService.Summary();
            if (toDoSummary.Status)
            {
                TaskBars.First(a => a.Title == "汇总").Content = toDoSummary.Result.Total.ToString();
                TaskBars.First(a => a.Title == "已完成").Content = toDoSummary.Result.CompleteCnt.ToString();
                TaskBars.First(a => a.Title == "完成比例").Content = toDoSummary.Result.CompleteRate.ToString();
            }
            var memoSummary = await memoService.Summary();
            if (memoSummary.Status)
            {
                TaskBars.First(a => a.Title == "备忘录").Content = memoSummary.Result.ToString();
            }
        }
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
           
            CreateTaskBars();
            GetSummary();

             //待办事项
             var para = new TodoParameter() { PageIndex = 0, PageSize = 100,Status=1 };
            var result=await toDoService.GetPageListAsync(para);
            if (result.Status)
            {
                ToDoDtos.Clear();
                 foreach(var dto in result.Result.Items)
                {
                    ToDoDtos.Add(dto);
                }
            }
            //备忘录
            var result2=await memoService.GetPageListAsync(para);
            if (result.Status)
            {
                MemoDtos.Clear();
                foreach(var dto in result2.Result.Items)
                {
                    MemoDtos.Add(dto);
                }
            }
        }
        #endregion
    }
}
