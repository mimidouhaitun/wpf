using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    public class SummaryDto
    {
#pragma warning disable
        public int Total { get; set; }
        public int CompleteCnt { get; set; }
        public string CompleteRate
        {
            get
            {
                if (Total == 0)
                {
                    return "";
                }
                else
                {
                    return ((CompleteCnt * 1.0) / Total) + "%";
                }
            }
        }
    }
}
