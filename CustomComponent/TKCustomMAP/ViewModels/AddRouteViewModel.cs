using System.Collections.ObjectModel;
using CustomComponent.TKCustomMAP.CustomPins;
using TK.CustomMap;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using TK.CustomMap.Overlays;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CustomComponent.TKCustomMAP.ViewModels
{
    public class AddRouteViewModel
    {
        GmsPlacePrediction  _fromPlace, _toPlace;
        Position _from, _to;

        public ObservableCollection<TKCustomMapPin> Pins { get; set; }
        public ObservableCollection<TKRoute> Routes { get; set; }
        public MapSpan Bounds { get; set; }

        /// <summary>
        /// Gets from place position .
        /// </summary>
        /// <value>From selected command.</value>
        public Command<IPlaceResult> FromSelectedCommand
        {
            get
            {
                return new Command<IPlaceResult>(async (p) =>
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                       // TKNativeiOSPlaceResult placeResult = (TKNativeiOSPlaceResult)p;
                        //_fromPlace = placeResult;
                        //_from = placeResult.Details.Coordinate;

                        var gmsResult = p as GmsPlacePrediction;
                        if (gmsResult != null)
                        {
                            _fromPlace = gmsResult;
                            var details = await GmsPlace.Instance.GetDetails(gmsResult.PlaceId);
                            _from = new Position(details.Item.Geometry.Location.Latitude, details.Item.Geometry.Location.Longitude);
                        }
                    }
                    else
                    {
                        var gmsResult = p as GmsPlacePrediction;
                        if (gmsResult != null)
                        {
                            _fromPlace = gmsResult;
                            var details = await GmsPlace.Instance.GetDetails(gmsResult.PlaceId);
                            _from = new Position(details.Item.Geometry.Location.Latitude,details.Item.Geometry.Location.Longitude);
                        }
                    }
                });
            }
        }
        /// <summary>
        /// Gets the to place position.
        /// </summary>
        /// <value>To selected command.</value>
        public Command<IPlaceResult> ToSelectedCommand
        {
            get
            {
                return new Command<IPlaceResult>(async (p) =>
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        //TKNativeiOSPlaceResult placeResult = (TKNativeiOSPlaceResult)p;
                        //_toPlace = placeResult;
                        //_to = placeResult.Details.Coordinate;
                        var gmsResult = p as GmsPlacePrediction;
                        if (gmsResult != null)
                        {
                            _toPlace = gmsResult;
                            var details = await GmsPlace.Instance.GetDetails(gmsResult.PlaceId);
                            _to = new Position(details.Item.Geometry.Location.Latitude, details.Item.Geometry.Location.Longitude);
                        }
                    }
                    else
                    {
                        var gmsResult = p as GmsPlacePrediction;
                        if (gmsResult != null)
                        {
                            _toPlace = gmsResult;
                            var details = await GmsPlace.Instance.GetDetails(gmsResult.PlaceId);
                            _to = new Position(details.Item.Geometry.Location.Latitude, details.Item.Geometry.Location.Longitude);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Calculate the route between two position.
        /// </summary>
        /// <value>The add route command.</value>
        public Command AddRouteCommand
        {
           get
            {
                return new Command(() =>
                {
                    if (_toPlace == null || _fromPlace == null) return;

                    var route = new TKRoute
                    {
                        TravelMode = TKRouteTravelMode.Driving,
                        Source = _from,
                        Destination = _to,
                        Color = Color.Red,
                        LineWidth=5
                    };

                    Pins.Add(new RoutePin
                    {
                        Route = route,
                        IsSource = true,
                        IsDraggable = true,
                        Position = _from,
                        Title = _fromPlace.Description,
                        ShowCallout = true,
                        DefaultPinColor = Color.Green
                    });
                    Pins.Add(new RoutePin
                    {
                        Route = route,
                        IsSource = false,
                        IsDraggable = true,
                        Position = _to,
                        Title = _toPlace.Description,
                        ShowCallout = true,
                        DefaultPinColor = Color.Red
                    });

                    Routes.Add(route);
                    Application.Current.MainPage.Navigation.PopAsync();
                });
            }
        }

        public AddRouteViewModel(ObservableCollection<TKRoute> routes, ObservableCollection<TKCustomMapPin> pins, MapSpan bounds)
        {
            Routes = routes;
            Pins = pins;
            Bounds = bounds;
        }
    }
}