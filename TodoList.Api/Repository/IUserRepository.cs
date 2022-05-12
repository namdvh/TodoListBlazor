using TodoList.Api.Entities;
using TodoList.Models;

namespace TodoList.Api.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();
    }
}
