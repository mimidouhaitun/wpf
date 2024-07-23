﻿using MyToDo.Common.Models;
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
        public MemoViewModel()
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            CreateToDoList();
            AddCommand = new DelegateCommand(Add);
        }

        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }
        public DelegateCommand AddCommand { get; set; }
        void CreateToDoList()
        {
            for (int i = 0; i < 20; i++)
            {
                MemoDtos.Add(new MemoDto() { Title = "标题" + i, Content = "测试数据..." });
            }
        }
    }
}
