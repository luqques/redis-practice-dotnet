using Microsoft.AspNetCore.Mvc;
using Redis.Practice.Api.Models;
using Redis.Practice.Api.Repositories;

namespace Redis.Practice.Api.Controllers
{
    [ApiController]
    [Route("v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            User? user = await _userRepository.GetUserAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("insert-user")]
        public async Task<ActionResult<User>> AddUser(UserDto user)
        {
            var newUser = await _userRepository.AddUserAsync(user);
            
            if (user == null)
                return BadRequest();

            return Ok(newUser);
        }
    }
}
