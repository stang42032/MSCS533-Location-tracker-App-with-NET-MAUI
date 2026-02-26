using LocationHeatMapApp.Models;

namespace LocationHeatMapApp.Services
{
    public class LocationService
    {
        private readonly DatabaseService _database;

        public LocationService(DatabaseService database)
        {
            _database = database;
        }

        public async Task TrackLocationAsync()
        {
            var request = new GeolocationRequest(
                GeolocationAccuracy.High,
                TimeSpan.FromSeconds(10));

            var location = await Geolocation.Default.GetLocationAsync(request);

            if (location != null)
            {
                await _database.AddLocationAsync(new LocationRecord
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    Timestamp = DateTime.Now
                });
            }
        }
    }
}