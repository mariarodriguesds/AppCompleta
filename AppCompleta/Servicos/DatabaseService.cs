using AppCompleta.Models;
using SQLite;

namespace AppCompleta.Servicos
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _database.Table<User>().ToListAsync();
        }

        public async Task<User> GetUserAsync(string login, string senha)
        {
            return await _database.Table<User>()
                .Where(u => u.Login == login && u.Senha == senha)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _database.Table<User>()
                .Where(u => u.Login == login)
                .FirstOrDefaultAsync();
        }
        public async Task<int> SaveUserAsync(User user)
        {
            return await _database.InsertAsync(user);
        }

    }
}