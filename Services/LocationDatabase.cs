using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationHeatMapApp.Services
{
    public class LocationPoint
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class LocationDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public LocationDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<LocationPoint>().Wait();
        }

        public Task<int> SaveLocationAsync(LocationPoint point)
        {
            return _database.InsertAsync(point);
        }

        public Task<List<LocationPoint>> GetLocationsAsync()
        {
            return _database.Table<LocationPoint>().ToListAsync();
        }
    }
}