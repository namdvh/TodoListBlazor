using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Models;

namespace TodoList.Api.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoListDbContext _context;

        public TaskRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task<Entities.Task> Create(Entities.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> Delete(Entities.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> GetById(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<TaskDto>> GetTaskList()
        {
            return await _context.Tasks.Include(x=>x.Assignee).Select(x => new TaskDto() { 
            Status=x.Status,
            Name=x.Name,
            AssigneeId=x.AssigneeId,
            CreatedDate=x.CreatedDate,
            Priority=x.Priority,
            AssigneeName=x.Assignee!=null ? x.Assignee.FirstName+' '+x.Assignee.LastName:"N/A",
            Id=x.Id}).ToListAsync();

        }

        public async Task<Entities.Task> Update(Entities.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
