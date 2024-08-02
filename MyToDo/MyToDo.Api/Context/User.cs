using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Api.Context
{
    public class User: BaseEntity
    {
#pragma warning disable
        public string Account { get; set; }
        public string? UserName { get; set; }
        public string PassWord { get; set; }
    }
}
