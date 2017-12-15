using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CustomComponent.Helpers;
using CustomComponent.SearchNearBy.Models;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using CustomComponent.SearchNearBy.Services;
using System.Linq;
using System.IO;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Splat;
using TK.CustomMap.Overlays;

namespace CustomComponent.SearchNearBy.ViewModels
{
    public class DashBoardViewModel : DashboardModel
    {
        MapSpan _mapRegion = MapSpan.FromCenterAndRadius(new Position(17.4354, 78.3827), Distance.FromKilometers(2));
        TKCustomMapPin _selectedPin;
        ObservableCollection<TKCustomMapPin> _custompins;
        ObservableCollection<DashboardModel> _nearbyresults;
        public ObservableCollection<TKRoute> Routes { get; set; }
        public DashBoardViewModel()
        {
            GetCurrentLocation();
            GetNearByPlaces("Fetching nearby companies...", Constant.NearbyUrl, "companies", "It companies", Constant.APIKey);
        }

        public async void GetCurrentLocation()
        {
            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                    return;

                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetLastKnownLocationAsync();

                if (position == null)
                {
                    return;
                }

                Lat = position.Latitude;
                Long = position.Longitude;

            }
            catch
            {

            }
            finally
            {

            }

        }

        /// <summary>
        /// Gets the schools nearby.
        /// </summary>
        /// <value>The button one command.</value>
        public Command BtnOneCommand
        {
            get
            {
                return new Command(() => GetNearByPlaces("Fetching nearby schools...", Constant.NearbyUrl, "school", "school", Constant.APIKey));

            }

        }

        /// <summary>
        /// Gets the restaurant nearby.
        /// </summary>
        /// <value>The button two command.</value>
        public Command BtnTwoCommand
        {
            get
            {
                return new Command(() => GetNearByPlaces("Fetching nearby restaurant...", Constant.NearbyUrl, "restaurant", "Restaurant", Constant.APIKey));

            }
        }

        /// <summary>
        /// Gets the malls nearby.
        /// </summary>
        /// <value>The button two command.</value>
        public Command BtnThreeCommand
        {
            get
            {
                return new Command(() => GetNearByPlaces("Fetching nearby malls...", Constant.NearbyUrl, "shopping malls", "mall", Constant.APIKey));

            }

        }


        bool _isenabled = false;
        public bool IsEnabled
        {
            get { return _isenabled; }
            set
            {
                if (_isenabled != value)
                {
                    _isenabled = value;
                    OnPropertyChanged("IsEnabled");
                }
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
        /// Custom Pins bound to <see cref="TKCustomMap"/>
        /// </summary>
        public ObservableCollection<TKCustomMapPin> CustomPins
        {
            get { return _custompins; }
            set
            {
                if (_custompins != value)
                {
                    _custompins = value;
                    OnPropertyChanged("CustomPins");
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
        /// Selected pin bound to the <see cref="TKCustomMap"/>
        /// </summary>
        public ObservableCollection<DashboardModel> NearByReaults
        {
            get { return _nearbyresults; }
            set
            {
                if (_nearbyresults != value)
                {
                    _nearbyresults = value;
                    OnPropertyChanged("NearByReaults");
                }
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
                    if (pin != null)
                    {
                        MapRegion = MapSpan.FromCenterAndRadius(pin.Position, Distance.FromKilometers(1));
                    }
                });
            }
        }
        string itemselected;
        public string ItemSelected
        {
            get { return itemselected; }
            set
            {
                itemselected = value;
                OnPropertyChanged();
            }
        }

        public Command ItemSelectedCommand => new Command((s)=>
        {
            if (Routes==null)
            {
                Routes = new ObservableCollection<TKRoute>();
            }
            if (s!=null)
            {
                Routes.Clear();
                var selecteditem = s as DashboardModel;
                var route = new TKRoute
                {
                    TravelMode = TKRouteTravelMode.Driving,
                    Source = new Position(17.4354, 78.3827),
                    Destination = new Position(selecteditem.Lat, selecteditem.Long),
                    Color = Color.Red,
                    LineWidth = 5
                };
                Routes.Add(route);
            }


        });


        public async void GetNearByPlaces(String busytext, string nearbyurl, string type, string searchkeyword, string apikey)
        {
         try
            {

                // var loadingImage = await BitmapLoader.Current.LoadFromResource("mapIcon.png", 100, 100);
                //UserDialogs.Instance.ShowImage(loadingImage, "Please Wait...", 10000);
                UserDialogs.Instance.ShowLoading(busytext,MaskType.Gradient);
                IList<Photo> photoReference;
                var NearbyPredictions = await GmsPlace.Instance.GetNearByPredictions(FormattedURL(nearbyurl, type, searchkeyword, apikey));
                ObservableCollection<DashboardModel> NearByResultItems = new ObservableCollection<DashboardModel>();
                ObservableCollection<TKCustomMapPin> customPinList = new ObservableCollection<TKCustomMapPin>();

                int i = 1;
                foreach (var item in NearbyPredictions.PredictionsNearBy)
                {
                    customPinList.Add(new TKCustomMapPin
                    {
                        Title = item.Name,
                        ShowCallout = true,
                        //Image = item.Icon,
                        IsCalloutClickable = true,
                        Position = new Position(item.Geometry.Location.Latitude, item.Geometry.Location.Longitude),
                    });
                    byte[] PredictionImageArray = null;
                    //if (true)
                    //{
                    i++;
                    photoReference = item.photos;
                    PredictionImageArray = await GmsPlace.Instance.GetPredictionImage(FormattedImageURL(Constant.ImageUrl, 200.ToString(), photoReference == null ? "" : photoReference[0].PhotoReference, apikey));

                    //}

                    NearByResultItems.Add(new DashboardModel()
                    {
                        Name = item.Name,
                        Rating ="Rating: " +item.Rating,
                        Address = item.Address,
                        Lat=item.Geometry.Location.Latitude,
                        Long=item.Geometry.Location.Longitude,
                        Status=item?.opening_hours?.open_now==true?"Open Now":"Closed",
                        ImgSource = PredictionImageArray==null?"icon.png": ImageSource.FromStream(() => new MemoryStream(PredictionImageArray)) 

                    });

                }
                //_custompins.Clear();
                CustomPins = customPinList;
                NearByReaults = NearByResultItems;
            }
            catch (Exception ex)
            {

            }
            finally 
            {
                UserDialogs.Instance.HideLoading();  
            }
        }
        /// <summary>
        /// Build the query string for predictions
        /// </summary>
        /// <returns>The URL.</returns>
        /// <param name="nearbyurl">Nearbyurl.</param>
        /// <param name="type">Type.</param>
        /// <param name="searchkeyword">Searchkeyword.</param>
        /// <param name="apikey">Apikey.</param>
        public string FormattedURL(string nearbyurl, string type, string searchkeyword, string apikey)
        {
            return string.Format("{0}&type={1}&keyword={2}&key={3}", nearbyurl, type, searchkeyword, apikey);
        }

        /// <summary>
        /// Formatteds the image URL.
        /// </summary>
        /// <returns>The image URL.</returns>
        /// <param name="imageurl">Imageurl.</param>
        /// <param name="width">Width.</param>
        /// <param name="photoreference">Photoreference.</param>
        /// <param name="apikey">Apikey.</param>

        public string FormattedImageURL(string imageurl,string width, string photoreference,  string apikey)
        {
            return string.Format("{0}?maxwidth={1}&photoreference={2}&key={3}", imageurl,width, photoreference,apikey);
        }

      

    }
}
