using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TodoList.Api.Data;
using TodoList.Api.Extensions;
using TodoList.Api.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<TodoListDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.SetIsOriginAllowed((host) => true)
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader()
                                                    .AllowCredentials());
});
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();


var app = builder.Build();
app.MigrateDbContext<TodoListDbContext>((context, services) =>
{
    var logger = services?.GetService<ILogger<TodoListDbContextSeed>>();
    new TodoListDbContextSeed().SeedAsync(context, logger).Wait();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
