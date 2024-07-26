using RestSharp;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class MyHttpRestClient
    {
        private readonly RestClient restClient;
        public MyHttpRestClient()
        {
            this.restClient = new RestClient("http://localhost:5057/");
        }

        public async Task<ApiResponse> ExecuteAsync(RequestConfig baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", "application/json");
            if (request.Parameters != null)
            {
                request.AddParameter("param", System.Text.Json.JsonSerializer.Serialize(baseRequest.Parameter));
            }
            var response = await restClient.ExecuteAsync(request);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(RequestConfig baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", "application/json");
            if (request.Parameters != null)
            {
                request.AddParameter("param", System.Text.Json.JsonSerializer.Serialize(baseRequest.Parameter));
            }
            var response = await restClient.ExecuteAsync(request);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
        }
    }
}
