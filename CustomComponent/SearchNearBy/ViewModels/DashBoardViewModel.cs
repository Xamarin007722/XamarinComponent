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
using System.IO;
using Acr.UserDialogs;
using TK.CustomMap.Overlays;
using CustomComponent.SearchNearBy.Pages;
using Rg.Plugins.Popup.Extensions;

namespace CustomComponent.SearchNearBy.ViewModels
{
    /// <summary>
    /// Dash board view model class.
    /// </summary>
    public class DashBoardViewModel : DashboardModel
    {
        MapSpan _mapRegion = MapSpan.FromCenterAndRadius(new Position(17.4354, 78.3827), Distance.FromKilometers(2));
        TKCustomMapPin _selectedPin;
        ObservableCollection<TKCustomMapPin> _custompins;
        ObservableCollection<DashboardModel> _nearbyresults;
        public ObservableCollection<TKRoute> _routes;

        ObservableCollection<TKCustomMapPin> _pins;
        ObservableCollection<TKCircle> _circles;
        //ObservableCollection<TKPolyline> _lines;

        string _firstSearchtext= string.Empty, _secondSearchtext= string.Empty, _thirdSearchtext= string.Empty, _fourthSearchtext = string.Empty;
        public DashBoardViewModel()
        {
          try
            {
                SettingsDataModel model = new SettingsDataModel();
                model = App.SettingDatabase.GetSettings();

                SrchBtnOne = model?.FirstBtnText;
                SrchBtnTwo = model?.SecondBtnText;
                SrchBtnThree = model?.ThirdBtnText;
                SrchBtnFour = model?.FourthBtnText;

                _firstSearchtext = model?.FirstSearchText;
                _secondSearchtext = model?.SecondBtnText;
                _thirdSearchtext = model?.ThirdBtnText;
                _fourthSearchtext = model?.FourthBtnText;

               // _listRefreshCommand = new Command(() =>RefreshList());
                model?.Dispose();
                GetCurrentLocation();
                GetNearByPlaces("Fetching nearby companies...", Constant.NearbyUrl, "companies", "It companies", Constant.APIKey);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Refreshs the list using PullToRefresh.
        /// </summary>
        public void RefreshList()
        {
            IsBusy = true;
            GetNearByPlaces("Fetching nearby companies...", Constant.NearbyUrl, "companies", "It companies", Constant.APIKey);
            IsBusy = false;
        }
        /// <summary>
        /// Gets the current GPS location.
        /// </summary>
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
                return new Command(() => GetNearByPlaces("Fetching nearby " + SrchBtnOne + "...", Constant.NearbyUrl, SrchBtnOne, _firstSearchtext, Constant.APIKey));
            }

        }

        /// <summary>
        /// Gets the restaurant nearby.Pins
        /// </summary>
        /// <value>The button two command.</value>
        public Command BtnTwoCommand
        {
            get
            {
                return new Command(() => GetNearByPlaces("Fetching nearby " + SrchBtnTwo + "...", Constant.NearbyUrl, SrchBtnTwo,_secondSearchtext, Constant.APIKey));

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
                return new Command(() => GetNearByPlaces("Fetching nearby "+ SrchBtnThree+"...", Constant.NearbyUrl, SrchBtnThree,_thirdSearchtext, Constant.APIKey));
            }
        }

        /// <summary>
        /// Gets the button four command.
        /// </summary>
        /// <value>The button four command.</value>
        public Command BtnFourCommand
        {
            get
            {
                return new Command(() => GetNearByPlaces("Fetching nearby " + SrchBtnFour + "...", Constant.NearbyUrl, SrchBtnFour, _fourthSearchtext, Constant.APIKey));
            }
        }

        /// <summary>
        /// The list refresh command.
        /// </summary>
        public Command ListRefreshCommand
        {
            get
            {
                return new Command(() => RefreshList());
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
        /// The isenabled property.
        /// </summary>
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
                  try
                    {

                    // move to the bounds of the route
                        MapRegion = r.Bounds;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    finally 
                    {
                        UserDialogs.Instance.HideLoading();
                    }
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

        ///// <summary>
        ///// Lines bound to the <see cref="TKCustomMap"/>
        ///// </summary>
        //public ObservableCollection<TKPolyline> Lines
        //{
        //    get { return _lines; }
        //    set
        //    {
        //        if (_lines != value)
        //        {
        //            _lines = value;
        //            OnPropertyChanged("Lines");
        //        }
        //    }
        //}

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

        /// <summary>
        /// Gets the item selected command.
        /// </summary>
        /// <value>The item selected command.</value>
        public Command ItemSelectedCommand => new Command((s)=>
        {
            try
            {
                if (Routes == null)
                {
                    Routes = new ObservableCollection<TKRoute>();
                }
                if (s != null)
                {
                    Device.BeginInvokeOnMainThread(() => UserDialogs.Instance.ShowLoading("Please Wait...", MaskType.Gradient));
                    Routes.Clear();
                    var selecteditem = s as DashboardModel;
                    var route = new TKRoute
                    {
                        TravelMode = TKRouteTravelMode.Walking,
                        Source = new Position(Lat, Long),
                        Destination = new Position(selecteditem.Lat, selecteditem.Long),
                        Color = Color.FromHex("#0080ff"),
                        LineWidth = 13,
                    };

                    //CustomPins.Add(new RoutePin
                    //{
                    //    Route = route,
                    //    IsSource = true,
                    //    IsDraggable = true,
                    //    Position = new Position(Lat, Long),
                    //    ShowCallout = true,
                    //    DefaultPinColor = Color.Green
                    //});
                    //CustomPins.Add(new RoutePin
                    //{
                    //    Route = route,
                    //    IsSource = false,
                    //    IsDraggable = true,
                    //    Position = new Position(selecteditem.Lat, selecteditem.Long),
                    //    ShowCallout = true,
                    //    DefaultPinColor = Color.Red
                    //});


                    Routes.Add(route);
                    RouteCalculationFinishedCommand.Execute(route);

                    //GmsDirectionResult Routeresult = await GmsDirection.
                    //Instance.CalculateRoute(new Position(Lat, Long),
                    //                        new Position(selecteditem.Lat, selecteditem.Long), GmsDirectionTravelMode.Walking, null);

                    //var routes = Routeresult.Routes.FirstOrDefault();

                    //Lines = new ObservableCollection<TKPolyline>();
                    //Pins = new ObservableCollection<TKCustomMapPin>();
                    //IEnumerable<Position> pos = routes.Polyline.Positions;

                    //var line = new TKPolyline
                    //{
                    //    Color = Color.Green,
                    //    LineWidth = 10f,
                    //    LineCoordinates = pos.ToList()
                    //};

                    //Pins.Add(new TKCustomMapPin
                    //{
                    //    //Route = route,
                    //    //IsSource = true,
                    //    IsDraggable = true,
                    //    Position = new Position(Lat, Long),
                    //    ShowCallout = true,
                    //    DefaultPinColor = Color.Green
                    //});
                    //Pins.Add(new TKCustomMapPin
                    //{
                    //    //Route = route,
                    //    //IsSource = false,
                    //    IsDraggable = true,
                    //    Position = new Position(selecteditem.Lat, selecteditem.Long),
                    //    ShowCallout = true,
                    //    DefaultPinColor = Color.Red
                    //});

                    //Lines.Add(line);

                }
            }
            catch (Exception ex)
            {

            }
            finally 
            {
                UserDialogs.Instance.HideLoading(); 
            }
        });

        /// <summary>
        /// Gets the near by places as per searchkeyword value.
        /// </summary>
        /// <param name="busytext">Busytext.</param>
        /// <param name="nearbyurl">Nearbyurl.</param>
        /// <param name="type">Type.</param>
        /// <param name="searchkeyword">Searchkeyword.</param>
        /// <param name="apikey">Apikey.</param>
        public async void GetNearByPlaces(String busytext, string nearbyurl, string type, string searchkeyword, string apikey)
        {
         try
            {
                if (Routes!=null)
                {
                    Routes.Clear();
                }
                // var loadingImage = await BitmapLoader.Current.LoadFromResource("mapIcon.png", 100, 100);
                //UserDialogs.Instance.ShowImage(loadingImage, "Please Wait...", 10000);
                UserDialogs.Instance.ShowLoading(busytext,MaskType.Gradient);
                IList<Photo> photoReference;
                var NearbyPredictions = await SearchNearBy.Services.GmsPlace.Instance.GetNearByPredictions(FormattedURL(nearbyurl, type, searchkeyword, apikey));
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
                    PredictionImageArray = await SearchNearBy.Services.GmsPlace.Instance.GetPredictionImage(FormattedImageURL(Constant.ImageUrl, 300.ToString(), photoReference == null ? "" : photoReference[0].PhotoReference, apikey));

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
        /// Move to ImageLargeView which opens as popUp page.
        /// </summary>
        /// <value>The image tapped command.</value>
        public Command ImageTappedCommand
        {
            get
            {
                return new Command(async(s) =>
                {
                    var model = s as DashboardModel;
                    await Application.Current.MainPage. Navigation.PushPopupAsync(new ImageLargeView(model.ImgSource));
                });

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
