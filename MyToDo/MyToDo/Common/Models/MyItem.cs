using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    public class MyItem
    {
        public string DisplayName { get; set; }
        public int Value { get; set; } // 这是你想要获取的“value” 
    }
    public class StatusItem
    {
        public string DisplayName { get; set; }
        public StatusEnum Value { get; set; } // 这是你想要获取的“value” 
    }
}
