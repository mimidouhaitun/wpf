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
    public class ToDoController : ControllerBase
    {
        private readonly ITodoService todoService;

        public ToDoController(ITodoService todoService) 
        {
            this.todoService = todoService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetFirstOrDefault(int id)
        {
           return await todoService.GetSingleAsync(id);
        }

        [HttpGet]
        public async Task<ApiResponse<IPagedList<ToDoDto>>> GetPageList([FromQuery] QueryParameter query)
        {
            return await todoService.GetPageListAsync(query);
        }

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDoDto model)
        {
            return await todoService.AddAsync(model);
        }

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDoDto model)
        {
            return await todoService.UpdateAsync(model);
        }

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id)
        {
            return await todoService.DeleteAsync(id);
        }
    }
}
