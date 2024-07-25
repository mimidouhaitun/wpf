
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;
using MyToDo.Api.Context.Repository;
using MyToDo.Api.Extensions;
using MyToDo.Api.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyToDoContext>(options =>
{
    var connstr = builder.Configuration.GetConnectionString("ToDoConnection");
    options.UseSqlite(connstr);
}).AddUnitOfWork<MyToDoContext>()
.AddCustomRepository<User,UserRepository>()
.AddCustomRepository<ToDo,ToDoRepository>()
.AddCustomRepository<Memo,MemoRepository>()
;

builder.Services.AddTransient<ITodoService, ToDoService>();

var mapper = MyAutoMapperConfig.CreateConfiguration().CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
