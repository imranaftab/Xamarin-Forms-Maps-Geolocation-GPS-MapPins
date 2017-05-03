using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapsDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            btnGetGeolocation.Clicked += BtnGetGeolocation_Clicked;
            UpdateMap(new Position(54.0103973, -2.7899181));
        }

        private async void BtnGetGeolocation_Clicked(object sender, EventArgs e)
        {
            var position = await GetGeolocation();
            UpdateMap(position);
            SetPin(position, txtName.Text, txtEmail.Text );
            
        }

        private void SetPin(Position position, string name, string email)
        {
            if (String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(email))
            {
                name = MainPage.Name;
                email = MainPage.Email;
            }
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label =name,
                Address = email
            };
            MyMap.Pins.Add(pin);
        }

        private void UpdateMap(Position position)
        {
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));

        }

        private async Task<Position> GetGeolocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(10000);
            return new Position(position.Latitude, position.Longitude); 
        }

        private static string Name = "Imran Aftab Rana";
        private static string Email = "i.rana@lancs.ac.uk";
    }
}
