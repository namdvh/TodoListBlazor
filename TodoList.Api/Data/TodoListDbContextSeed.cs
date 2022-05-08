using Microsoft.AspNetCore.Identity;
using TodoList.Api.Entities;
using TodoList.Api.Enumerable;

namespace TodoList.Api.Data
{
    public class TodoListDbContextSeed
    {
        public readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public async System.Threading.Tasks.Task SeedAsync(TodoListDbContext context, ILogger<TodoListDbContextSeed> logger)
        {
            if (!context.Tasks.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Hoai Nam",
                    LastName = "Doan Vu",
                    Email = "admin1@gmail.com",
                    PhoneNumber = "0123456789",
                    UserName = "admin"
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123$");
                context.Users.Add(user);
            }
            if (!context.Tasks.Any())
            {
                context.Tasks.Add(new Entities.Task()
                {
                    Id = Guid.NewGuid(),
                    Name = "Same Task 1",
                    CreatedDate = DateTime.Now,
                    Priority = Priority.High,
                    Status = Status.Open
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
