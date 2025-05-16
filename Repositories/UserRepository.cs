using Redis.Practice.Api.Models;

namespace Redis.Practice.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
