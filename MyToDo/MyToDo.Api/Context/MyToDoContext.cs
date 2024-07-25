using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Api.Context
{
    public class MyToDoContext:DbContext
    {
#pragma warning disable
        public MyToDoContext(DbContextOptions<MyToDoContext> options):base(options) 
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Memo> Memos { get; set; }
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnableAutoHistory();
        }
    }
}
