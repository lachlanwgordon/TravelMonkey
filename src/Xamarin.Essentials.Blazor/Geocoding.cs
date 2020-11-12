using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geocoding.Microsoft;

namespace Xamarin.EssentialsBL.Blazor
{
    public class GeocodingImpl : Xamarin.Essentials.Interfaces.IGeocoding
    {
        private static string APIKey = "";//donot commit this
        //private static string APIKey = "";//donot commit this
        private static BingMapsGeocoder Geocoder = new BingMapsGeocoder(APIKey);
        public static void Initialize(string aPIKey)
        {
            APIKey = aPIKey;
        }

        public GeocodingImpl()
        {
        }

        public async Task<IEnumerable<Essentials.Location>> GetLocationsAsync(string address)
        {
            var bingAddresses = await Geocoder.GeocodeAsync(address);
            var locations = bingAddresses.Select(x => new Essentials.Location(x.Coordinates.Latitude, x.Coordinates.Longitude));
            return locations;
        }

        public async Task<IEnumerable<Essentials.Placemark>> GetPlacemarksAsync(Essentials.Location location)
        {
            var bingAddresses = await Geocoder.ReverseGeocodeAsync(new Geocoding.Location(location.Latitude, location.Longitude));
            var placeMarks = bingAddresses.Select(x =>
                new Essentials.Placemark
                {
                    AdminArea = x.AdminDistrict,
                    CountryName = x.CountryRegion,
                    Locality = x.Locality,
                    PostalCode = x.PostalCode,
                    FeatureName = "",
                    Location = new Essentials.Location(x.Coordinates.Latitude, x.Coordinates.Longitude),
                    CountryCode = "",
                    SubAdminArea = x.AdminDistrict2,
                    SubLocality = x.Neighborhood,
                    Thoroughfare = x.AddressLine
                }) ;
            return placeMarks;
        }

        public Task<IEnumerable<Essentials.Placemark>> GetPlacemarksAsync(double latitude, double longitude)
        {
            return GetPlacemarksAsync(new Essentials.Location(latitude, longitude));
        }
    }
}
