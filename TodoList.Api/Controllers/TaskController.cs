using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Repository;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var task = await _taskRepository.GetTaskList();
            return Ok(task);
        }
        [HttpPost]
        //tao moi ban ghi dung httppost
        public async Task<IActionResult> Create(TaskCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var task = await _taskRepository.Create(new Entities.Task()
            {
               Name=request.Name,
               Priority=request.Priority,
               Status=Models.Enumerable.Status.Open,
               CreatedDate=DateTime.Now,
               Id=request.Id,
            });
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }
        //api/tasks/xxxx
        [HttpGet]
        [ActionName(nameof(GetById))]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null) return NotFound($"{id} is not found");
            return Ok(new TaskDto()
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Id,
                AssigneeId = task.AssigneeId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate,
            });
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult>Update(Guid id,Entities.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            var taskFromDb = await _taskRepository.GetById(id);
            if (task == null) return NotFound($"{id} is not found");
            taskFromDb.Name= task.Name;
            var taskRs = await _taskRepository.Update(taskFromDb);
            return Ok(new TaskDto()
            {
                Name= taskRs.Name,
                Status= taskRs.Status,
                Id= taskRs.Id,
                AssigneeId= taskRs.AssigneeId,
                Priority= taskRs.Priority,
                CreatedDate= taskRs.CreatedDate,
            });
        }
    }
}
