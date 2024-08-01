using MyToDo.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface IMemoService:IBaseService<MemoDto>
    {
        Task<ApiResponse<int>> Summary();
    }
    public class MemoService : BaseService<MemoDto>, IMemoService
    {
        public MemoService(MyHttpRestClient restClient) : base(restClient, "memo")
        {
        }

        public async Task<ApiResponse<int>> Summary()
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{controllerName}/Summary";
            return await restClient.ExecuteAsync<int>(request);
        }
    }
}
