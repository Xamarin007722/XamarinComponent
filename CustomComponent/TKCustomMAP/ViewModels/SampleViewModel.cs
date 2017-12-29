using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using TK.CustomMap.Api.OSM;
using TK.CustomMap.Interfaces;
using TK.CustomMap.Overlays;
using CustomComponent.TKCustomMAP.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using TK.CustomMap;
using CustomComponent.SearchNearBy.Pages;
using CustomComponent.TKCustomMAP.CustomPins;
using CustomComponent.Views;

namespace CustomComponent.TKCustomMAP.ViewModels
{
    public class SampleViewModel : INotifyPropertyChanged
    {
        TKTileUrlOptions _tileUrlOptions;

        MapSpan _mapRegion = MapSpan.FromCenterAndRadius(new Position(17.4474, 78.3762), Distance.FromKilometers(2));
        Position _mapCenter;
        TKCustomMapPin _selectedPin;
        bool _isClusteringEnabled;
        ObservableCollection<TKCustomMapPin> _pins;
        ObservableCollection<TKRoute> _routes;
        ObservableCollection<TKCircle> _circles;
        ObservableCollection<TKPolyline> _lines;
        ObservableCollection<TKPolygon> _polygons;
       


        public Command ShowListCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (_pins == null || !_pins.Any())
                    {
                        Application.Current.MainPage.DisplayAlert("Nothing there!", "No pins to show!", "OK");
                        return;
                    }
                    var listPage = new PinListPage(Pins);
                    listPage.PinSelected += async (o, e) =>
                    {
                        SelectedPin = e.Pin;
                        await Application.Current.MainPage.Navigation.PopAsync();
                    };
                    await Application.Current.MainPage.Navigation.PushAsync(listPage);
                });
            }
        }

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
        /// Gets the go to near by command.
        /// </summary>
        /// <value>The go to near by command.</value>
        public Command GoToNearByCommand
        {
            get
            {
                return new Command(async()=> await Application.Current.MainPage.Navigation.PushAsync(new DashBoard()));
            }
        }
        /// <summary>
        /// Gets the go to pins command.
        /// </summary>
        /// <value>The go to pins command.</value>
        public Command GoToPinsCommand
        {
            get
            {
                return new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new MapView()));
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
                        var pin = new TKCustomMapPin
                        {
                            Position = position,
                            Title = string.Format("Pin {0}, {1}", position.Latitude, position.Longitude),
                            ShowCallout = true,
                            IsDraggable = true
                        };
                        _pins.Add(pin);
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
                            Title = details.Item.FormattedAddress,
                            ShowCallout = true,
                            IsDraggable = true
                        };
                        _pins.Add(pin);
                        MapRegion = MapSpan.FromCenterAndRadius(new Position(MapCenter.Latitude, MapCenter.Longitude), Distance.FromKilometers(1));
                        return;
                    }
                    //var osmResult = p as OsmNominatimResult;
                    //if (osmResult != null)
                    //{
                    //    MapCenter = new Position(osmResult.Latitude, osmResult.Longitude);
                    //    return;
                    //}

                    //if (Device.OS == TargetPlatform.Android)
                    //{
                    //    var prediction = (TKNativeAndroidPlaceResult)p;

                    //    var details = await TKNativePlacesApi.Instance.GetDetails(prediction.PlaceId);

                    //    MapCenter = details.Coordinate;
                    //}
                    //else if (Device.OS == TargetPlatform.iOS)
                    //{
                    //    var prediction = (TKNativeiOSPlaceResult)p;

                    //    MapCenter = prediction.Details.Coordinate;
                    //}
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
        /// Navigate to a new page to get route source/destination
        /// </summary>
        public Command AddRouteCommand
        {
            get            {
                return new Command(() =>
                {
                    if (Routes == null) Routes = new ObservableCollection<TKRoute>();

                    var addRoutePage = new AddRoutePage(Routes, Pins, MapRegion);
                    Application.Current.MainPage.Navigation.PushAsync(addRoutePage);
                });
            }
        }
        public Command SettingCommand
        {
            get
            {
                return new Command(() =>
                {
                    Application.Current.MainPage.Navigation.PushAsync(new SettingsPage());
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

        public Func<string, IEnumerable<TKCustomMapPin>, TKCustomMapPin> GetClusteredPin => (group, clusteredPins) =>
        {
            return null;
            //return new TKCustomMapPin
            //{
            //    DefaultPinColor = Color.Blue,
            //    Title = clusteredPins.Count().ToString(),
            //    ShowCallout = true
            //};
        };

        public SampleViewModel()
        {
            _mapCenter = new Position(17.4474, 78.3762);
            _pins = new ObservableCollection<TKCustomMapPin>();
            _circles = new ObservableCollection<TKCircle>();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}