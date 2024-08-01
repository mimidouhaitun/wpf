using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    public class ToDoSummaryDto:BindableBase
    {
#pragma warning disable
        private int total;
        private int completeCnt;
        private string completeRate;

        public int Total
        {
            get => total; 
            set
            {
                total = value; RaisePropertyChanged();
            }
        }
        public int CompleteCnt
        {
            get => completeCnt;
            set
            {
                completeCnt = value; RaisePropertyChanged();
            }
        }
        public string CompleteRate
        {
            get => completeRate;
            set
            {
                completeRate = value; RaisePropertyChanged();
            }
        } 
    }
}
