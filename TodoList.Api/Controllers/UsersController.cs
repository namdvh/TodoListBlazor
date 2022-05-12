using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Repository;
using TodoList.Models;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userRepository.GetUserList();
            var assignees = user.Select(x => new AssigneeDto()
            {
                Id = x.Id,
                FullName = x.FirstName + " " + x.LastName
            });
            return Ok(assignees);
        }
    }
}
