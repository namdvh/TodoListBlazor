using Microsoft.EntityFrameworkCore;
using TodoList.Api.Entities;
namespace TodoList.Api.Data
{
    public class TodoListDbContext:DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
        {

        }
        public DbSet<TodoList.Api.Entities.Task> Tasks { get; set; }
    } 
}
