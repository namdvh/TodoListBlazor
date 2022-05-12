using TodoList.Models;

namespace TodoListBlazor.Services
{
    public interface IUserApiClient
    {
        Task<List<AssigneeDto>> GetAssignee();
    }
}
