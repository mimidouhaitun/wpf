using MyToDo.Common.Models;
using MyToDo.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface IToDoService:IBaseService<ToDoDto>
    {

    }
    public class ToDoService :BaseService<ToDoDto>, IToDoService
    {
        public ToDoService(MyHttpRestClient restClient) : base(restClient, "todo")
        {
        }
    }
}
