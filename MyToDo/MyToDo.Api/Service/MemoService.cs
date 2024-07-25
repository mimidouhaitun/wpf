using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Dtos;
using MyToDo.Api.Parameters;

namespace MyToDo.Api.Service
{
    public interface IMemoService : IBaseService<MemoDto>
    {
    }

    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(MemoDto model)
        {
            var memo=mapper.Map<Memo>(model);
           unitOfWork.GetRepository<Memo>().Insert(memo);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return new ApiResponse(true, memo);
            }
            return new ApiResponse(false, "添加数据失败");
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var repository = unitOfWork.GetRepository<Memo>();
            var model=await repository.GetFirstOrDefaultAsync(predicate:a => a.Id == id);
            repository.Delete(model);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return new ApiResponse(true, model);
            }
            return new ApiResponse(false, "删除数据失败");
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            var repository = unitOfWork.GetRepository<Memo>();
            var pagedList =await repository.GetPagedListAsync(
                predicate:a=>string.IsNullOrWhiteSpace(query.Search)?true:a.Title.Contains(query.Search),
                pageSize:query.PageSize,pageIndex:query.PageIndex,
                orderBy:a=>a.OrderByDescending(b=>b.CreateDate));
            return new ApiResponse(true, pagedList);
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            var repository = unitOfWork.GetRepository<Memo>();
            var model=await repository.GetFirstOrDefaultAsync(predicate: a => a.Id == id);
            return new ApiResponse(true, model);
        }

        public async Task<ApiResponse> UpdateAsync(MemoDto model)
        {
            var repository = unitOfWork.GetRepository<Memo>();
            var old =await repository.GetFirstOrDefaultAsync(predicate: a => a.Id == model.Id);
            old.UpdateDate = DateTime.Now;
            old.Title= model.Title;
            old.Content = model.Content;
            repository.Update(old);
            await unitOfWork.SaveChangesAsync();
            return new ApiResponse(true, "更新成功");
        }
    }
}
