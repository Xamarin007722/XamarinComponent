using System.IO;
using System.Linq;
using Xamarin.Forms;
using TK.CustomMap;
using Xamarin.Forms.Maps;

namespace CustomComponent.Views
{
    public partial class MapView : ContentPage
    {
        public MapView()
        {
            InitializeComponent();

            var HitechCity = new Position(17.4474, 78.3762);
            var map = new TKCustomMap(MapSpan.FromCenterAndRadius(HitechCity, Distance.FromKilometers(2)));
            map.SetBinding(TKCustomMap.MapTypeProperty, "MapType");
            map.SetBinding(TKCustomMap.IsShowingUserProperty, "UserLocation");
            map.SetBinding(TKCustomMap.ShowTrafficProperty, "ShowTraffic");
            map.SetBinding(TKCustomMap.HasScrollEnabledProperty, "ScrolledEnabled");
            map.SetBinding(TKCustomMap.HasZoomEnabledProperty, "ZoomEnabled");
            map.SetBinding(TKCustomMap.MapClickedCommandProperty, "MapClickedCommand");
            map.SetBinding(TKCustomMap.MapLongPressCommandProperty, "MapLongPressCommand");
          
           

            buttonTakeSnapshot.Clicked += async (sender, e) =>
            {
                var vByte = await map.GetSnapshot();
                MemoryStream stream = new MemoryStream(vByte);
                imageSnapshot.Source = ImageSource.FromStream(() => stream);
            };


  ////////////////////// Set Pin /////////////////////////////////////////

            var position1 = new Position(17.4474, 78.3762);
            var position2 = new Position(17.4354, 78.3827);
            var position3 = new Position(17.4375, 78.4483);
          

            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position1,
                Label = "Hitech City",
                Address = "www.intilaq.tn",
            };

            var pin2 = new Pin
            {
                Type = PinType.Place,
                Position = position2,
                Label = "Inorbit Mall",
                Address = "www.groupe-telnet.com"
            };

            var pin3 = new Pin
            {
                Type = PinType.Place,
                Position = position3,
                Label = "Ameerpet",
                Address = "www.kromberg-schubert.com"
            };

            map.Pins.Add(pin1);
            map.Pins.Add(pin2);
            map.Pins.Add(pin3);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(position2, Distance.FromMeters((300))));


            // Get geocode 
            buttonGeocode.Clicked += async (sender, e) =>
            {
                var geocoder = new Geocoder();
                var positions = await geocoder.GetPositionsForAddressAsync(entryAddress.Text);
                if (positions.Count() > 0)
                {
                    var pos = positions.First();
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = pos,
                        Label = "Gachibowli",
                        Address = "Hyderabad",
                    };
                    map.Pins.Add(pin);
                   
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMiles((.3))));
                    var reg = map.VisibleRegion;
                    var format = "0.00";
                }
                else
                {
                    await this.DisplayAlert("Not found", "Geocoder returns no results", "Close");
                }
            };


            mapLayout.Children.Add(map);


            var vm = new ViewModel.MapViewViewModel();
            this.BindingContext = vm;

        }



    }
}

