using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Api.Context
{
    public class ToDo: BaseEntity
    {
#pragma warning disable
        public string Title {  get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
    }
}
