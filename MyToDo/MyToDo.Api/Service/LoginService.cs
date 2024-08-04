using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Dtos;
using MyToDo.Api.Extensions;

namespace MyToDo.Api.Service
{
    public interface ILoginService
    {
        Task<ApiResponse<UserDto>> LoginAsync(string account, string password);
        Task<ApiResponse> RegisgerAsync(UserDto userDto);
    }
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LoginService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<UserDto>> LoginAsync(string account, string password)
        {
            var repository=unitOfWork.GetRepository<User>();
            var md5Pwd = password.GetMd5();
            var user=await repository.GetFirstOrDefaultAsync(predicate: a => a.Account == account && a.PassWord == md5Pwd);
            if (user == null)
            {
                return new ApiResponse<UserDto>(false, "账号或者密码错误，请重试！");
            }
            else
            {
                var userDto = new UserDto();
                mapper.Map(user, userDto);
                return new ApiResponse<UserDto>(true, userDto);
            }
        }

        public async Task<ApiResponse> RegisgerAsync(UserDto userDto)
        {
            var repository = unitOfWork.GetRepository<User>();
            var user = await repository.GetFirstOrDefaultAsync(predicate: a => a.UserName == userDto.UserName);
            if (user != null) { 
                return new ApiResponse(false, "用户账号已存在，请重新注册");
            }
            else
            {
                user=mapper.Map<User>(userDto);
                user.PassWord = user.PassWord.GetMd5();
                repository.Insert(user);
                unitOfWork.SaveChanges();
                return new ApiResponse(true, user);
            }
        }
    }
}
