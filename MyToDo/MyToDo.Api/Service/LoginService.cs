using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Dtos;

namespace MyToDo.Api.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(string account, string password);
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
        public async Task<ApiResponse> LoginAsync(string account, string password)
        {
            var repository=unitOfWork.GetRepository<User>();
            var user=await repository.GetFirstOrDefaultAsync(predicate: a => a.Account == account && a.PassWord == password);
            if (user == null)
            {
                return new ApiResponse(false, "账号或者密码错误，请重试！");
            }
            else
            {
                return new ApiResponse(true, user);
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
                repository.Insert(user);
                unitOfWork.SaveChanges();
                return new ApiResponse(true, user);
            }
        }
    }
}
