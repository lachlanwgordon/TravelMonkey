using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Xamarin.Essentials.Blazor
{
    public class Geolocation : Interfaces.IGeolocation
    {
        private readonly IJSRuntime _jsRuntime;

        public Geolocation(IJSRuntime jsRuntime)
        {
            Console.WriteLine($"GeolocationImpl .ctor");
            _jsRuntime = jsRuntime;
        }

        public async Task<Location> GetLastKnownLocationAsync()
        {
            return Location;
        }

        public async Task<Location> GetLocationAsync()
        {
            Console.WriteLine($"GetLocationAsync");
            try
            {
                var loc = await _jsRuntime.InvokeAsync<string>("GeoLocationInterop.getLocation", DotNetObjectReference.Create(this));
                Console.WriteLine($"loc: {loc}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}\n{ex.StackTrace}");
            }

            var waitCount = 0;
            while (Location == null && waitCount < 1000)
            {
                Debug.WriteLine("Waiting for location");
                await Task.Delay(50);
                waitCount += 50;
            }


            if (Location != null)
            {
                Console.WriteLine($"Location found {Location.Latitude}");

                return Location;

            }
            else
            {
                Console.WriteLine($"No location");
                return new Location();
            }


        }

        [JSInvokable]
        public async Task LocationCallBack(Location loc)
        {
            Console.WriteLine($"JS says {loc}");
            Location = loc;
            //Location = loc;
        }


        Location Location;

        public Task<Location> GetLocationAsync(GeolocationRequest request)
        {
            return GetLocationAsync();

        }

        public Task<Location> GetLocationAsync(GeolocationRequest request, CancellationToken cancelToken)
        {
            return GetLocationAsync();
        }
    }
}
