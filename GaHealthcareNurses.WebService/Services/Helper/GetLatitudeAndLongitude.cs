using System;
using System.Collections.Generic;
using System.Text;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.ViewModels;
using GoogleMaps.LocationServices;


namespace Services.Helper
{
    public static class GetLatitudeAndLongitude
    {
        public static LatitudeAndLongitude GetLatLogFromAddress(string addressLine1, string city, string state, int postalCode)
        {
            AddressData staticAddress = new AddressData
            {
                Address = addressLine1,
                City = city,
                State = state,
                Country = "US",
                Zip = postalCode.ToString()
            };

            var gls = new GoogleLocationService(apikey: "AIzaSyDxyylk-TqR4sFkJFC0qT5IjBpoXJx7Czk");

                MapPoint latlong = gls.GetLatLongFromAddress(staticAddress);
                LatitudeAndLongitude latitudeAndLongitude = new LatitudeAndLongitude
                {
                    Latitude = latlong?.Latitude.ToString(),
                    Longitude = latlong?.Longitude.ToString()
                };
                //System.Console.WriteLine("Address ({0}) is at {1},{2}", staticAddress, latitude, longitude);
                return latitudeAndLongitude;

        }

        public static string GetAddressFromLatLong(double latitude,double longitude)
        {
            var gls = new GoogleLocationService(apikey: "AIzaSyDxyylk-TqR4sFkJFC0qT5IjBpoXJx7Czk");
            var address = gls.GetAddressFromLatLang(latitude, longitude);
          var test=  address.ToString();
            return null;
        }
    }
}