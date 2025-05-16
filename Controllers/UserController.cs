using Microsoft.AspNetCore.Mvc;
using Redis.Practice.Api.Models;
using Redis.Practice.Api.Repositories;

namespace Redis.Practice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await _userRepository.GetUserAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
