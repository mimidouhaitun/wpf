using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Dtos;
using MyToDo.Api.Parameters;
using MyToDo.Api.Service;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService memoService;

        public MemoController(IMemoService memoService) 
        {
            this.memoService = memoService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetFirstOrDefault(int id)
        {
           return await memoService.GetSingleAsync(id);
        }
        [HttpGet]
        public async Task<ApiResponse<IPagedList<MemoDto>>> GetPageList([FromQuery] QueryParameter query)
        {
            return await memoService.GetPageListAsync(query);
        }
        [HttpGet]
        public ApiResponse<int> Summary()
        {
            return memoService.Summary();
        }

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] MemoDto model)
        {
            return await memoService.AddAsync(model);
        }
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] MemoDto model)
        {
            return await memoService.UpdateAsync(model);
        }
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id)
        {
            return await memoService.DeleteAsync(id);
        }
    }
}
