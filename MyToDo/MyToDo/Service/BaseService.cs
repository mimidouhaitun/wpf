using MyToDo.Parameters;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface IBaseService<T> where T : class
    {
        Task<ApiResponse<T>> AddAsync(T entity);
        Task<ApiResponse<T>> UpdateAsync(T entity);
        Task<ApiResponse> DeleteAsync(int id);
        Task<ApiResponse<T>> GetFirstOrDefaultAsync(int id);
        Task<ApiResponse<PagedList<T>>> GetPageListAsync(QueryParameter parameter);
    }
    public class BaseService<T> : IBaseService<T> where T : class
    {
        public readonly MyHttpRestClient restClient;
        public readonly string controllerName;

        public BaseService(MyHttpRestClient restClient, string controllerName)
        {
            this.restClient = restClient;
            this.controllerName = controllerName;
        }
        public async Task<ApiResponse<T>> AddAsync(T entity)
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Post;
            request.StringBody = JsonConvert.SerializeObject(entity);
            request.Route = $"api/{controllerName}/Add";
            return await restClient.ExecuteAsync<T>(request);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Delete;
            request.Route = $"api/{controllerName}/Delete?id={id}";
            return await restClient.ExecuteAsync(request);
        }

        public async Task<ApiResponse<PagedList<T>>> GetPageListAsync(QueryParameter parameter)
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{controllerName}/GetPageList?PageIndex={parameter.PageIndex}" +
                $"&PageSize={parameter.PageSize}&Search={parameter.Search}";
            return await restClient.ExecuteAsync<PagedList<T>>(request);
        }

        public async Task<ApiResponse<T>> GetFirstOrDefaultAsync(int id)
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{controllerName}/GetFirstOrDefault?id={id}";
            return await restClient.ExecuteAsync<T>(request);
        }

        public async Task<ApiResponse<T>> UpdateAsync(T entity)
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Post;
            request.StringBody = JsonConvert.SerializeObject(entity);
            request.Route = $"api/{controllerName}/Update";
            return await restClient.ExecuteAsync<T>(request);
        }
    }
}
