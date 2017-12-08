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

namespace CustomComponent.SearchNearBy.ViewModels
{
    public class DashBoardViewModel:DashboardModel
    {
        MapSpan _mapRegion = MapSpan.FromCenterAndRadius(new Position(17.4354, 78.3827), Distance.FromKilometers(2));
        TKCustomMapPin _selectedPin;
        ObservableCollection<TKCustomMapPin> _custompins;
        ObservableCollection<DashboardModel> _nearbyresults;
        public DashBoardViewModel()
        {
           // GetNearByPlaces();
        }

        public async void GetNearbySchools()
        {
            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                    return;

                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy =50;

                var position = await locator.GetLastKnownLocationAsync();

                if (position == null)
                {
                    return;
                }

                Lat = position.Latitude.ToString();
                Long = position.Longitude.ToString();

            }
            catch
            {

            }
            finally
            {

            }

        }


        public Command BtnOneCommand
        {
            get
            {

                    return new Command(() => GetNearByPlaces(Constant.NearbyUrl, "school", "school", Constant.APIKey));
              
            }

        }
        public Command BtnTwoCommand
        {
            get
            {
                return new Command(() => GetNearByPlaces(Constant.NearbyUrl, "restaurant", "Restaurant", Constant.APIKey));

            }
        }
        public Command BtnThreeCommand
        {
            get
            {
                return new Command(() => GetNearByPlaces(Constant.NearbyUrl, "malls", "mall", Constant.APIKey));

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
                    if (pin!=null)
                    {
                        MapRegion = MapSpan.FromCenterAndRadius(pin.Position, Distance.FromKilometers(1));
                    }
                });
            }
        }


        public async void GetNearByPlaces(string nearbyurl, string type, string searchkeyword, string apikey)
        {
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
                    Image = item.Icon,
                    IsCalloutClickable = true,
                    Position = new Position(item.Geometry.Location.Latitude, item.Geometry.Location.Longitude),
                });
                byte[] PredictionImageArray=null ;
                //if (true)
                //{
                    i++;
                    photoReference = item.photos;
                PredictionImageArray = await GmsPlace.Instance.GetPredictionImage(FormattedImageURL(Constant.ImageUrl, 200.ToString(),photoReference==null?"": photoReference[0].PhotoReference, apikey));

                //}

                NearByResultItems.Add(new DashboardModel()
                {
                    Name = item.Name,
                    Rating = item.Rating,
                    Address = item.Address,
                    ImgSource = ImageSource.FromStream(() => new MemoryStream(PredictionImageArray)) == null ? "icon.png" : ImageSource.FromStream(() => new MemoryStream(PredictionImageArray))

                });
               
            }
            //_custompins.Clear();
            CustomPins = customPinList;
            NearByReaults = NearByResultItems;
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
