using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Xamarin.Essentials.Blazor
{
    public class GoogleHttpGeocoding : Xamarin.Essentials.Interfaces.IGeocoding
    {
        public HttpClient HttpClient { get; }
        public object GeoCodingAPIKEY = "";//Move to config

        public GoogleHttpGeocoding(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }


        public async Task<IEnumerable<Xamarin.Essentials.Location>> GetLocationsAsync(string address)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={GeoCodingAPIKEY}";
            var res = await HttpClient.GetStringAsync(url);
            var googleLocs = JsonConvert.DeserializeObject<APIResult>(res);
            Debug.WriteLine(res);

            var locs = googleLocs.results.Select(x => new Location(x.geometry.location.lat, x.geometry.location.lng));

            return locs;
        }

        public Task<IEnumerable<Placemark>> GetPlacemarksAsync(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Placemark>> GetPlacemarksAsync(Essentials.Location location)
        {
            throw new NotImplementedException();
        }
    }


    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }



    public class Bounds
    {
        public GoogleLocation northeast { get; set; }
        public GoogleLocation southwest { get; set; }
    }

    public class GoogleLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }



    public class Geometry
    {
        public Bounds bounds { get; set; }
        public GoogleLocation location { get; set; }
        public string location_type { get; set; }
        public Bounds viewport { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public bool partial_match { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }

    public class APIResult
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}
