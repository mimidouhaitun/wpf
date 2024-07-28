using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Dtos;
using MyToDo.Api.Parameters;
using System.Net.WebSockets;

namespace MyToDo.Api.Service
{
    public interface ITodoService:IBaseService<ToDoDto>
    {
        Task<ApiResponse<IPagedList<ToDoDto>>> GetPageListAsync(ToDoParameter query);
    }

    public class ToDoService : ITodoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ToDoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(ToDoDto model)
        {
            var todo=mapper.Map<ToDo>(model);
           unitOfWork.GetRepository<ToDo>().Insert(todo);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return new ApiResponse(true, todo);
            }
            return new ApiResponse(false, "添加数据失败");
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var repository = unitOfWork.GetRepository<ToDo>();
            var model=await repository.GetFirstOrDefaultAsync(predicate:a => a.Id == id);
            repository.Delete(model);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return new ApiResponse(true, model);
            }
            return new ApiResponse(false, "删除数据失败");
        }

        public async Task<ApiResponse<IPagedList<ToDoDto>>> GetPageListAsync(QueryParameter query)
        {
            return await Task.Run(() =>
            {
                return new ApiResponse<IPagedList<ToDoDto>>(true, new MyPageList<ToDoDto>());
            });
            
        }
        public async Task<ApiResponse<IPagedList<ToDoDto>>> GetPageListAsync(ToDoParameter query)
        {
            var repository = unitOfWork.GetRepository<ToDo>();
            var pagedList = await repository.GetPagedListAsync(
                predicate: a => (string.IsNullOrWhiteSpace(query.Search) ? true : a.Title.Contains(query.Search))
                && (query.Status == 0 ? true : a.Status == query.Status),
                pageSize: query.PageSize, pageIndex: query.PageIndex,
                orderBy: a => a.OrderByDescending(b => b.CreateDate));
            var pagedList1 = PagedList.From<ToDoDto, ToDo>(pagedList, (todos) =>
            {
                var todoDtos = new List<ToDoDto>();
                foreach (var todo in todos)
                {
                    var todoDto = mapper.Map<ToDoDto>(todo);
                    todoDtos.Add(todoDto);
                }
                return todoDtos;
            });
            return new ApiResponse<IPagedList<ToDoDto>>(true, pagedList1);
        }
        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            var repository = unitOfWork.GetRepository<ToDo>();
            var model=await repository.GetFirstOrDefaultAsync(predicate: a => a.Id == id);
            return new ApiResponse(true, model);
        }

        public async Task<ApiResponse> UpdateAsync(ToDoDto model)
        {
            var repository = unitOfWork.GetRepository<ToDo>();
            var old =await repository.GetFirstOrDefaultAsync(predicate: a => a.Id == model.Id);
            old.UpdateDate = DateTime.Now;
            old.Status = model.Status;
            old.Title= model.Title;
            old.Content = model.Content;
            repository.Update(old);
            await unitOfWork.SaveChangesAsync();
            var dto = mapper.Map<ToDoDto>(old);
            return new ApiResponse(true, dto);
        }
    }

    public class MyPageList<T> : IPagedList<T>
    {
        public int IndexFrom => 0;

        public int PageIndex => 0;

        public int PageSize => 10;

        public int TotalCount =>0;

        public int TotalPages => 0;

        public IList<T> Items => new List<T>();

        public bool HasPreviousPage => true;

        public bool HasNextPage => true;
    }
}
