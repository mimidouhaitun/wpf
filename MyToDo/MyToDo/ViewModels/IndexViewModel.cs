using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Views.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class IndexViewModel:BindableBase
    {
        #region 字段
        private ObservableCollection<TaskBar> taskBars;
        private ObservableCollection<ToDoDto> toDoDtos;
        private ObservableCollection<MemoDto> memoDtos;
        private readonly IMyDialogHelperService myDialogHelper;
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
        #endregion 属性

        #region 方法
        public IndexViewModel(IMyDialogHelperService myDialogHelper)
        {
            TaskBars = new ObservableCollection<TaskBar>();
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            CreateTaskBars();
            this.myDialogHelper = myDialogHelper;
        }

        private void Execute(string obj)
        {
            if (obj == "新增备忘录")
            {
                AddMemo();
            }else if (obj == "新增待办")
            {
                AddToDo();
            }
        }

        private void AddToDo()
        {
            //Root,和MainWindow.xaml中的<materialDesign:DialogHost下的Identifier="Root"属性保持一致
            myDialogHelper.ShowDialog("AddToDoView",null,"Root");
        }

        private void AddMemo()
        {
            myDialogHelper.ShowDialog("AddMemoView", null, "Root");
        }

        void CreateTaskBars()
        {
            TaskBars.Add(new TaskBar() { Icom = "ClockFast", Title = "汇总", Content = "9", Color = "#FF0CA0FF", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icom = "ClockCheckOutline", Title = "已完成", Content = "9", Color = "#FF1ECA3A", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icom = "ChartLineVariant", Title = "完成比例", Content = "100%", Color = "#FF02C6DC", Target = "" });
            TaskBars.Add(new TaskBar() { Icom = "PlaylistStar", Title = "备忘录", Content = "19", Color = "#FFFFA000", Target = "MemoView" });
        }
        void CreateTestData()
        {
            
            for (int i = 0; i < 10; i++) 
            {
                ToDoDtos.Add(new ToDoDto() { Title = "待办" + i, Content = "正在处理中..." });
                MemoDtos.Add(new MemoDto() { Title = "备忘" + i, Content = "我的密码" });
            }
        }
        #endregion
    }
}
