using Redis.Practice.Api.Models;

namespace Redis.Practice.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> AddUserAsync(UserDto user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
