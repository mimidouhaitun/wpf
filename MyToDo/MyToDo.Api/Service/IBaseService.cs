using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using MyToDo.Api.Parameters;

namespace MyToDo.Api.Service
{
    public interface IBaseService<T>
    {
        Task<ApiResponse<IPagedList<T>>> GetPageListAsync(QueryParameter query);
        Task<ApiResponse> GetSingleAsync(int id);
        Task<ApiResponse> AddAsync(T model);
        Task<ApiResponse> UpdateAsync(T model);
        Task<ApiResponse> DeleteAsync(int id);
    }
}
