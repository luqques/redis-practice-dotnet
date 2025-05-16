using Dapper;
using Npgsql;
using Redis.Practice.Api.Caching;
using Redis.Practice.Api.Models;
using System.Text.Json;

namespace Redis.Practice.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string? _connectionString;
        private readonly ICachingService _cachingService;

        public UserRepository(IConfiguration configuration, ICachingService cachingService)
        {
            _cachingService = cachingService ?? throw new ArgumentNullException(nameof(cachingService));
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

        public async Task<User?> AddUserAsync(UserDto user)
        {
            const string sql = "INSERT INTO users (name) VALUES (@Name) RETURNING id, name;";
            using var connection = GetConnection();

            User? newUser =  await connection.QuerySingleOrDefaultAsync<User>(sql, user);

            if (newUser == null)
                return null;

            await _cachingService.SetCache($"user:{newUser.Id}", JsonSerializer.Serialize(newUser));

            return newUser;
        }

        public async Task DeleteUserAsync(int id)
        {
            const string sql = "DELETE FROM users WHERE id = @Id;";
            using var connection = GetConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            const string sql = "SELECT id, name FROM users;";
            using var connection = GetConnection();
            return await connection.QueryAsync<User>(sql);
        }

        public async Task<User?> GetUserAsync(int id)
        {
            var user = await _cachingService.GetCache($"user:{id}");
            if (user != null)
                return JsonSerializer.Deserialize<User>(user);

            const string sql = "SELECT id, name FROM users WHERE id = @Id;";
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task UpdateUserAsync(User user)
        {
            const string sql = "UPDATE users SET name = @Name WHERE id = @Id;";
            using var connection = GetConnection();
            await connection.ExecuteAsync(sql, user);
        }
    }
}
