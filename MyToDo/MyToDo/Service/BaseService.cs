using MyToDo.Parameters;
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
        private readonly MyHttpRestClient restClient;
        private readonly string controllerName;

        public BaseService(MyHttpRestClient restClient, string controllerName)
        {
            this.restClient = restClient;
            this.controllerName = controllerName;
        }
        public async Task<ApiResponse<T>> AddAsync(T entity)
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Post;
            request.Parameter = entity;
            request.Route = $"api/{controllerName}/Add";
            return await restClient.ExecuteAsync<T>(request);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Delete;
            request.Parameter = id;
            request.Route = $"api/{controllerName}/Delete";
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
            request.Parameter = id;
            request.Route = $"api/{controllerName}/Get";
            return await restClient.ExecuteAsync<T>(request);
        }

        public async Task<ApiResponse<T>> UpdateAsync(T entity)
        {
            RequestConfig request = new RequestConfig();
            request.Method = RestSharp.Method.Post;
            request.Parameter = entity;
            request.Route = $"api/{controllerName}/Update";
            return await restClient.ExecuteAsync<T>(request);
        }
    }
}
