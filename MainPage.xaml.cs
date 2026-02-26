using LocationHeatMapApp.Services;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace LocationHeatMapApp;

public partial class MainPage : ContentPage
{
    private DatabaseService _databaseService;
    private LocationService _locationService;

    public MainPage()
    {
        InitializeComponent();

        _databaseService = new DatabaseService();
        _locationService = new LocationService(_databaseService);
    }

    private async void OnStartTrackingClicked(object sender, EventArgs e)
    {
        await _locationService.TrackLocationAsync();
        await LoadPins();
    }

    private async Task LoadPins()
    {
        var locations = await _databaseService.GetLocationsAsync();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            MainMap.Pins.Clear();

            foreach (var loc in locations)
            {
                var pin = new Pin
                {
                    Label = "Visited",
                    Location = new Location(loc.Latitude, loc.Longitude)
                };

                MainMap.Pins.Add(pin);
            }
        });
    }
}