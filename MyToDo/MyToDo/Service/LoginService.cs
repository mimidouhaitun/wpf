using MyToDo.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface ILoginService: IBaseService<UserDto>
    {
        Task<ApiResponse> LoginAsync(UserDto userDto);
        Task<ApiResponse> Register(UserDto userDto);
    }
    public class LoginService : BaseService<UserDto>,ILoginService
    {
        public LoginService(MyHttpRestClient restClient) : base(restClient, "Login")
        {
        }

        public async Task<ApiResponse> LoginAsync(UserDto userDto)
        {
            ApiResponse apiResponse = new ApiResponse();
            var config = new RequestConfig();
            config.Method = RestSharp.Method.Post;
            config.Route = $"api/{controllerName}/Login";
            config.StringBody = JsonConvert.SerializeObject(userDto);
            var result = await restClient.ExecuteAsync(config);
            apiResponse = result;
            return apiResponse;
        }

        public async Task<ApiResponse> Register(UserDto userDto)
        {
            ApiResponse apiResponse = new ApiResponse();
            var config = new RequestConfig();
            config.Method = RestSharp.Method.Post;
            config.Route = $"api/{controllerName}/Register";
            config.StringBody = JsonConvert.SerializeObject(userDto);
            var result = await restClient.ExecuteAsync(config);
            return apiResponse = result;
        }
    }
}
