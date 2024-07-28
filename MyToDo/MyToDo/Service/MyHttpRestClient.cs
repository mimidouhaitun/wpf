using RestSharp;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class MyHttpRestClient
    {
        private readonly RestClient restClient;
        public MyHttpRestClient()
        {
            var options = new RestClientOptions("http://localhost:5057/")
            {
                Timeout = System.TimeSpan.FromSeconds(15),
            };
            this.restClient = new RestClient(options) ;
        }

        public async Task<ApiResponse> ExecuteAsync(RequestConfig baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", "application/json");
            if (baseRequest.Method == Method.Post && string.IsNullOrWhiteSpace(baseRequest.StringBody) == false)
            {
                request.AddStringBody(baseRequest.StringBody, ContentType.Json);
            }
            var response = await restClient.ExecuteAsync(request);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(RequestConfig baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Method == Method.Post && string.IsNullOrWhiteSpace( baseRequest.StringBody)==false)
            {
                request.AddStringBody(baseRequest.StringBody, ContentType.Json);
            }
            var response = await restClient.ExecuteAsync(request);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
        }
    }
}
