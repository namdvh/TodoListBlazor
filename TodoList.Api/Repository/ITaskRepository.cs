using TodoList.Models;
using Task = TodoList.Api.Entities.Task;
namespace TodoList.Api.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDto>> GetTaskList();
        Task<Task>Create(Task task);
        Task<Task> Update(Task task);
        Task<Task> Delete(Task task);
        Task<Task> GetById(Guid id);
    }
}
