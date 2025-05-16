using Dapper;
using Npgsql;
using Redis.Practice.Api.Models;

namespace Redis.Practice.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string? _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

        public async Task<User?> AddUserAsync(UserDto user)
        {
            const string sql = "INSERT INTO users (name) VALUES (@Name) RETURNING id, name;";
            using var connection = GetConnection();
            return await connection.QuerySingleOrDefaultAsync<User>(sql, user);
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
