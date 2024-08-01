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
        Task<ApiResponse<PagedList<ToDoDto>>> GetPageListAsync(TodoParameter parameter);
        Task<ApiResponse<ToDoSummaryDto>> Summary();
    }
    public class ToDoService :BaseService<ToDoDto>, IToDoService
    {
        public ToDoService(MyHttpRestClient restClient) : base(restClient, "todo")
        {
        }

        public async Task<ApiResponse<PagedList<ToDoDto>>> GetPageListAsync(TodoParameter parameter)
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{controllerName}/GetPageList?PageIndex={parameter.PageIndex}" +
                $"&PageSize={parameter.PageSize}&Search={parameter.Search}&Status={parameter.Status}";
            return await restClient.ExecuteAsync<PagedList<ToDoDto>>(request);
        }

        public async Task<ApiResponse<ToDoSummaryDto>> Summary()
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{controllerName}/Summary";
            return await restClient.ExecuteAsync<ToDoSummaryDto>(request);
        }

    }
}
