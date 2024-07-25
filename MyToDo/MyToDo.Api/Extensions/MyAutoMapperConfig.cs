using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Dtos;
using System.Security.AccessControl;

namespace MyToDo.Api.Extensions
{
    public static class MyAutoMapperConfig
    {
        public static MapperConfiguration CreateConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ToDo, ToDoDto>().ReverseMap();
            });
        }
    }
}
