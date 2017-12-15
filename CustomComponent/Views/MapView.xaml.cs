using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CustomComponent.Interface;
using Plugin.Screenshot;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace CustomComponent.Views
{
    public partial class MapView : ContentPage
    {
        public MapView()
        {
            InitializeComponent();
            var polyline1 = new Polyline();

            var mapTypeValues = new List<MapType>();
            foreach (var mapType in Enum.GetValues(typeof(MapType)))
            {
                mapTypeValues.Add((MapType)mapType);
                pickerMapType.Items.Add(Enum.GetName(typeof(MapType), mapType));
            }

            pickerMapType.SelectedIndexChanged += (sender, e) =>
            {
                map.MapType = mapTypeValues[pickerMapType.SelectedIndex];
            };
            pickerMapType.SelectedIndex = 0;

           
            switchMyLocationEnabled.Toggled += (sender, e) =>
            {
                map.MyLocationEnabled = e.Value;
            };
            switchMyLocationEnabled.IsToggled = map.MyLocationEnabled;

           
            switchIsTrafficEnabled.Toggled += (sender, e) =>
            {
                map.IsTrafficEnabled = e.Value;
            };
            switchIsTrafficEnabled.IsToggled = map.IsTrafficEnabled;

           
           
            switchCompassEnabled.Toggled += (sender, e) =>
            {
                map.UiSettings.CompassEnabled = e.Value;
            };
            switchCompassEnabled.IsToggled = map.UiSettings.CompassEnabled;



            // MyLocationButtonEnabled
            switchMyLocationButtonEnabled.Toggled += (sender, e) =>
            {
                map.UiSettings.MyLocationButtonEnabled = e.Value;
            };
            switchMyLocationButtonEnabled.IsToggled = map.UiSettings.MyLocationButtonEnabled;


           
            switchScrollGesturesEnabled.Toggled += (sender, e) =>
            {
                map.UiSettings.ScrollGesturesEnabled = e.Value;
            };
            switchScrollGesturesEnabled.IsToggled = map.UiSettings.ScrollGesturesEnabled;

          

          
            switchZoomControlsEnabled.Toggled += (sender, e) =>
            {
                map.UiSettings.ZoomControlsEnabled = e.Value;
            };
            switchZoomControlsEnabled.IsToggled = map.UiSettings.ZoomControlsEnabled;

           
            switchZoomGesturesEnabled.Toggled += (sender, e) =>
            {
                map.UiSettings.ZoomGesturesEnabled = e.Value;
            };
            switchZoomGesturesEnabled.IsToggled = map.UiSettings.ZoomGesturesEnabled;

           
            map.MapClicked += (sender, e) =>
            {
                var lat = e.Point.Latitude.ToString("0.000");
                var lng = e.Point.Longitude.ToString("0.000");
                this.DisplayAlert("MapClicked", $"{lat}/{lng}", "CLOSE");
            };


            map.MapLongClicked += (sender, e) =>
            {
                var lat = e.Point.Latitude.ToString("0.000");
                var lng = e.Point.Longitude.ToString("0.000");
                this.DisplayAlert("MapLongClicked", $"{lat}/{lng}", "CLOSE");
            };

            // Map MyLocationButton clicked
            map.MyLocationButtonClicked += (sender, args) =>
            {
                args.Handled = switchHandleMyLocationButton.IsToggled;
                if (switchHandleMyLocationButton.IsToggled)
                {
                    this.DisplayAlert("MyLocationButtonClicked",
                                 "If set MyLocationButtonClickedEventArgs.Handled = true then skip obtain current location",
                                 "OK");
                }
            };
             
            map.CameraChanged += (sender, args) =>
            {
                var p = args.Position;
                //Show the lat long
                //$"Lat={p.Target.Latitude:0.00}, Long={p.Target.Longitude:0.00}, Zoom={p.Zoom:0.00}, Bearing={p.Bearing:0.00}, Tilt={p.Tilt:0.00}";
            };
        
            // Snapshot
            buttonTakeSnapshot.Clicked += async (sender, e) =>
            {
                var stream = await map.TakeSnapshot();
                imageSnapshot.Source = ImageSource.FromStream(() => stream);
            };

            buttonSnapshot.Clicked+=async (sender,e) =>
            {
                imageSnapshot.Source = null;
                MemoryStream stream = null;
                stream = new MemoryStream(await DependencyService.Get<IScreenShotManager>().CaptureAsync());
                // stream = new MemoryStream(await CrossScreenshot.Current.CaptureAsync()); // using xam.plugin.screenshot
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


           
            polyline1.StrokeWidth = 5f;
            polyline1.StrokeColor = Color.Blue;
            //polyline1.Positions.Add(position1);
            //polyline1.Positions.Add(position2);
            //polyline1.Positions.Add(position3);
            //map.Polylines.Add(polyline1);
            //map.Polylines.Add(CreateShiftedPolyline(polyline1, 0d, 0.05d, Color.Yellow));
            //map.Polylines.Add(CreateShiftedPolyline(polyline1, 0d, 0.10d, Color.Green));


            map.MoveToRegion(MapSpan.FromCenterAndRadius(position2, Distance.FromMeters((300))));


            // Geocode
            buttonGeocode.Clicked += async (sender, e) =>
            {
                var geocoder = new Xamarin.Forms.GoogleMaps.Geocoder();
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
                    polyline1.Positions.Add(pos);
                    polyline1.Positions.Add(pos);
                    map.Polylines.Add(polyline1);
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMiles((.3))));
                    var reg = map.VisibleRegion;
                    var format = "0.00";
                }
                else
                {
                    await this.DisplayAlert("Not found", "Geocoder returns no results", "Close");
                }
            };

        }
        private Polyline CreateShiftedPolyline(Polyline polyline, double shiftLat, double shiftLon, Color color)
        {
            var poly = new Polyline();
            poly.StrokeWidth = polyline.StrokeWidth;
            poly.StrokeColor = color;

            foreach (var p in polyline.Positions)
            {
                poly.Positions.Add(new Position(p.Latitude + shiftLat, p.Longitude + shiftLon));
            }

            return poly;
        }
    }
}

