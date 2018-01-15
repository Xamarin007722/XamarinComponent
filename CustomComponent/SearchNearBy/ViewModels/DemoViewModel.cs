using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmHelpers;
using TK.CustomMap;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using TK.CustomMap.Overlays;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CustomComponent.SearchNearBy.ViewModels
{
    /// <summary>
    /// Demo view model.
    /// </summary>
    public class DemoViewModel:BaseViewModel
    {
      
        /// <summary>
        /// The map and fileds declaration region.
        /// </summary>
        MapSpan _mapRegion = MapSpan.FromCenterAndRadius(new Position(17.4474, 78.3762), Distance.FromKilometers(2));
        Position _mapCenter;
        TKCustomMapPin _selectedPin;
        bool _isClusteringEnabled;
        ObservableCollection<TKCustomMapPin> _pins;
        ObservableCollection<TKRoute> _routes;
        ObservableCollection<TKCircle> _circles;
        ObservableCollection<TKPolyline> _lines;
        ObservableCollection<TKPolygon> _polygons;

        /// <summary>
        /// Map region bound to <see cref="TKCustomMap"/>
        /// </summary>
        public MapSpan MapRegion
        {
            get { return _mapRegion; }
            set
            {
                if (_mapRegion != value)
                {
                    _mapRegion = value;
                    OnPropertyChanged("MapRegion");
                }
            }
        }

        /// <summary>
        /// Pins bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public ObservableCollection<TKCustomMapPin> Pins
        {
            get { return _pins; }
            set
            {
                if (_pins != value)
                {
                    _pins = value;
                    OnPropertyChanged("Pins");
                }
            }
        }

        /// <summary>
        /// Routes bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public ObservableCollection<TKRoute> Routes
        {
            get { return _routes; }
            set
            {
                if (_routes != value)
                {
                    _routes = value;
                    OnPropertyChanged("Routes");
                }
            }
        }

        /// <summary>
        /// Circles bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public ObservableCollection<TKCircle> Circles
        {
            get { return _circles; }
            set
            {
                if (_circles != value)
                {
                    _circles = value;
                    OnPropertyChanged("Circles");
                }
            }
        }

        /// <summary>
        /// Lines bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public ObservableCollection<TKPolyline> Lines
        {
            get { return _lines; }
            set
            {
                if (_lines != value)
                {
                    _lines = value;
                    OnPropertyChanged("Lines");
                }
            }
        }

        /// <summary>
        /// Polygons bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public ObservableCollection<TKPolygon> Polygons
        {
            get { return _polygons; }
            set
            {
                if (_polygons != value)
                {
                    _polygons = value;
                    OnPropertyChanged("Polygons");
                }
            }
        }

        /// <summary>
        /// Map center bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public Position MapCenter
        {
            get { return _mapCenter; }
            set
            {
                if (_mapCenter != value)
                {
                    _mapCenter = value;
                    OnPropertyChanged("MapCenter");
                }
            }
        }

        /// <summary>
        /// Selected pin bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public TKCustomMapPin SelectedPin
        {
            get { return _selectedPin; }
            set
            {
                if (_selectedPin != value)
                {
                    _selectedPin = value;
                    OnPropertyChanged("SelectedPin");
                }
            }
        }




        /// <summary>
        /// Map Long Press bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public Command<Position> MapLongPressCommand
        {
            get
            {
                return new Command<Position>(async position =>
                {
                    var action = await Application.Current.MainPage.DisplayActionSheet(
                        "Long Press",
                        "Cancel",
                        null,
                        "Add Pin",
                        "Add Circle");

                    if (action == "Add Pin")
                    {
                        //var pin = new TKCustomMapPin
                        //{
                        //    Position = position,
                        //    Title = string.Format("Pin {0}, {1}", position.Latitude, position.Longitude),
                        //    ShowCallout = true,
                        //    IsDraggable = true
                        //};
                        //_pins.Add(pin);

                        Getroute();
                    }
                    else if (action == "Add Circle")
                    {
                        var circle = new TKCircle
                        {
                            Center = position,
                            Radius = 10000,
                            Color = Color.FromRgba(100, 0, 0, 80)
                        };
                        _circles.Add(circle);
                    }
                });
            }
        }

        /// <summary>
        /// Map Clicked bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public Command<Position> MapClickedCommand
        {
            get
            {
                return new Command<Position>((positon) =>
                {
                    SelectedPin = null;

                    // Determine if a point was inside a circle
                    if ((from c in _circles let distanceInMeters = c.Center.DistanceTo(positon) * 1000 where distanceInMeters <= c.Radius select c).Any())
                    {
                        Application.Current.MainPage.DisplayAlert("Circle tap", "Circle was tapped", "OK");
                    }
                });
            }
        }

        /// <summary>
        /// Command when a place got selected
        /// </summary>
        public Command<IPlaceResult> PlaceSelectedCommand
        {
            get
            {
                return new Command<IPlaceResult>(async p =>
                {
                    var gmsResult = p as GmsPlacePrediction;
                    if (gmsResult != null)
                    {
                        var details = await GmsPlace.Instance.GetDetails(gmsResult.PlaceId);
                        MapCenter = new Position(details.Item.Geometry.Location.Latitude, details.Item.Geometry.Location.Longitude);
                        _pins.Clear();
                        var pin = new TKCustomMapPin
                        {
                            Position = new Position(MapCenter.Latitude, MapCenter.Longitude),
                            Title = string.Format("Pin {0}, {1}", MapCenter.Latitude, MapCenter.Longitude),
                            ShowCallout = true,
                            IsDraggable = true
                        };
                        _pins.Add(pin);
                        MapRegion = MapSpan.FromCenterAndRadius(new Position(MapCenter.Latitude, MapCenter.Longitude), Distance.FromKilometers(1));
                        return;
                    }

                });
            }
        }

        /// <summary>
        /// Pin Selected bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public Command PinSelectedCommand
        {
            get
            {
                return new Command<TKCustomMapPin>((TKCustomMapPin pin) =>
                {
                    MapRegion = MapSpan.FromCenterAndRadius(SelectedPin.Position, Distance.FromKilometers(1));
                });
            }
        }


        /// <summary>
        /// Callout clicked bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public Command CalloutClickedCommand
        {
            get
            {
                return new Command<TKCustomMapPin>(async (TKCustomMapPin pin) =>
                {
                    var action = await Application.Current.MainPage.DisplayActionSheet(
                        "Callout clicked",
                        "Cancel",
                        "Remove Pin");

                    if (action == "Remove Pin")
                    {
                        _pins.Remove(pin);
                    }
                });
            }
        }

        /// <summary>
        /// Clear the map pins,route etc.
        /// </summary>
        /// <value>The clear map command.</value>
        public Command ClearMapCommand
        {
            get
            {
                return new Command(() =>
                {
                    _pins.Clear();
                    _circles.Clear();
                    if (_routes != null)
                        _routes.Clear();
                });
            }
        }





        /// <summary>
        /// Command when a route calculation finished
        /// </summary>
        public Command<TKRoute> RouteCalculationFinishedCommand
        {
            get
            {
                return new Command<TKRoute>(r =>
                {
                    // move to the bounds of the route
                    MapRegion = r.Bounds;
                });
            }
        }

        public  DemoViewModel()
        {
          
        }

        /// <summary>
        /// Getroute/Draw route between two given position.
        /// </summary>
        public async void Getroute()
        {
            GmsDirectionResult Routeresult= await  GmsDirection.
            Instance.CalculateRoute(new Position(17.4474, 78.3762),
            new Position(17.4375, 78.4483),GmsDirectionTravelMode.Driving,null);

            var routes= Routeresult.Routes.FirstOrDefault();
           
            Lines = new ObservableCollection<TKPolyline>();
            Pins = new ObservableCollection<TKCustomMapPin>();
            IEnumerable<Position> pos= routes.Polyline.Positions;

            var line = new TKPolyline
            {
                Color = Color.Green,
                LineWidth = 10f,
                LineCoordinates = pos.ToList()
            };

            Pins.Add(new TKCustomMapPin
            {
                //Route = route,
                //IsSource = true,
                IsDraggable = true,
                Position = new Position(17.4474, 78.3762),
                ShowCallout = true,
                DefaultPinColor = Color.Green
            });
            Pins.Add(new TKCustomMapPin
            {
                //Route = route,
                //IsSource = false,
                IsDraggable = true,
                Position = new Position(17.4375, 78.4483),
                ShowCallout = true,
                DefaultPinColor = Color.Red
            });
           
            Lines.Add(line);



            //var route = new TKRoute
            //{
            //    TravelMode = TKRouteTravelMode.Driving,
            //    Source = new Position(17.4474, 78.3762),
            //    Destination = new Position(17.4375,78.4483),
            //    Color = Color.Red,
            //    LineWidth = 5
            //};
            //Routes = new ObservableCollection<TKRoute>();
            //Pins = new ObservableCollection<TKCustomMapPin>();
            //Pins.Add(new RoutePin
            //{
            //    Route = route,
            //    IsSource = true,
            //    IsDraggable = true,
            //    Position = new Position(17.4474, 78.3762),
            //    ShowCallout = true,
            //    DefaultPinColor = Color.Green
            //});
            //Pins.Add(new RoutePin
            //{
            //    Route = route,
            //    IsSource = false,
            //    IsDraggable = true,
            //    Position = new Position(17.4375, 78.4483),
            //    ShowCallout = true,
            //    DefaultPinColor = Color.Red
            //});

            //Routes.Add(route);
        }


    }
}
