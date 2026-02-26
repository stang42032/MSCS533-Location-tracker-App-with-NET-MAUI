using SQLite;
using LocationHeatMapApp.Models;

namespace LocationHeatMapApp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public async Task InitAsync()
        {
            if (_database != null)
                return;

            var path = Path.Combine(FileSystem.AppDataDirectory, "locations.db");
            _database = new SQLiteAsyncConnection(path);

            await _database.CreateTableAsync<LocationRecord>();
        }

        public async Task AddLocationAsync(LocationRecord record)
        {
            await InitAsync();
            await _database.InsertAsync(record);
        }

        public async Task<List<LocationRecord>> GetLocationsAsync()
        {
            await InitAsync();
            return await _database.Table<LocationRecord>().ToListAsync();
        }
    }
}