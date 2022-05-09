using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Repository;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;
using AutoMapper;
using TodoList.Api.Data;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _taskRepositorymap;
        private readonly TodoListDbContext _context;

        public TaskController(ITaskRepository taskRepository,IMapper mapper, TodoListDbContext context)
        {
            _taskRepository = taskRepository;
            _taskRepositorymap= mapper;
            _context = context;
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
            //var task = await _taskRepository.Create(new Entities.Task()
            //{
            //   Name=request.Name,
            //   Priority=request.Priority,
            //   Status=Models.Enumerable.Status.Open,
            //   CreatedDate=DateTime.Now,
            //   Id=request.Id,
            //});
            else
            {
                Entities.Task task= _taskRepositorymap.Map<Entities.Task>(request);
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
            }
            return Ok();
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
        public async Task<IActionResult>  Update(Guid id,Entities.Task task)
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
