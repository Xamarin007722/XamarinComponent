using System;
using Xamarin.Forms;
using PropertyChanged;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using System.IO;
using CustomComponent.Interface;

namespace CustomComponent.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MapViewViewModel
    {
        public MapViewViewModel()
        {
            GetMapType();
        }


        bool _userLocationToggled = false;
        /// <summary>
        /// MapTrafficToggled property bound to ISToggled property of Show traffic Switch 
        /// </summary>
        public bool UserLocationToggled
        {
            get { return _userLocationToggled; }
            set
            {
                if (_userLocationToggled != value)
                {
                    _userLocationToggled = value;
                    UserLocation = value;
                }
            }
        }


        bool _userLocation = false;
        /// <summary>
        /// Map traffic property bound to the ShowTrafficProperty of "TKCustomMap"
        /// </summary>
        public bool UserLocation
        {
            get { return _userLocation; }
            set
            {
                if (_userLocation != value)
                {
                    _userLocation = value;
                }
            }
        }


        bool _mapZoomEnabled = false;
        /// <summary>
        /// MapTrafficToggled property bound to ISToggled property of Show traffic Switch 
        /// </summary>
        public bool MapZoomEnabled
        {
            get { return _mapZoomEnabled; }
            set
            {
                if (_mapZoomEnabled != value)
                {
                    _mapZoomEnabled = value;
                    ZoomEnabled = value;
                }
            }
        }


        bool _zoomEnabled = false;
        /// <summary>
        /// Map traffic property bound to the ShowTrafficProperty of "TKCustomMap"
        /// </summary>
        public bool ZoomEnabled
        {
            get { return _zoomEnabled; }
            set
            {
                if (_zoomEnabled != value)
                {
                    _zoomEnabled = value;
                }
            }
        }






        bool _mapScrollToggled = false;
        /// <summary>
        /// MapTrafficToggled property bound to ISToggled property of Show traffic Switch 
        /// </summary>
        public bool MapScrollToggled
        {
            get { return _mapScrollToggled; }
            set
            {
                if (_mapScrollToggled != value)
                {
                    _mapScrollToggled = value;
                    ScrolledEnabled = value;
                }
            }
        }


        bool _scrolledEnabled = false;
        /// <summary>
        /// Map traffic property bound to the ShowTrafficProperty of "TKCustomMap"
        /// </summary>
        public bool ScrolledEnabled
        {
            get { return _scrolledEnabled; }
            set
            {
                if (_scrolledEnabled != value)
                {
                    _scrolledEnabled = value;
                }
            }
        }



        bool _mapTrafficToggled = false;
        /// <summary>
        /// MapTrafficToggled property bound to ISToggled property of Show traffic Switch 
        /// </summary>
        public bool MapTrafficToggled
        {
            get { return _mapTrafficToggled; }
            set
            {
                if (_mapTrafficToggled != value)
                {
                    _mapTrafficToggled = value;
                    ShowTraffic = value;
                }
            }
        }


        bool _showTraffic=false;
        /// <summary>
        /// Map traffic property bound to the ShowTrafficProperty of "TKCustomMap"
        /// </summary>
        public bool ShowTraffic
        {
            get { return _showTraffic; }
            set
            {
                if (_showTraffic != value)
                {
                    _showTraffic = value;
                }
            }
        }




        MapType _mapValueSelected ;
        /// <summary>
        /// MapTrafficToggled property bound to ISToggled property of Show traffic Switch 
        /// </summary>
        public MapType MapValueSelected
        {
            get { return _mapValueSelected; }
            set
            {
                if (_mapValueSelected != value)
                {
                    _mapValueSelected = value;
                    MapType = value;
                }
            }
        }


        MapType _mapType ;
        /// <summary>
        /// Map traffic property bound to the ShowTrafficProperty of "TKCustomMap"
        /// </summary>
        public MapType MapType
        {
            get { return _mapType; }
            set
            {
                if (_mapType != value)
                {
                    _mapType = value;
                }
            }
        }

        ImageSource _imgSource;
        /// <summary>
        /// Map traffic property bound to the ShowTrafficProperty of "TKCustomMap"
        /// </summary>
        public ImageSource ImgSource
        {
            get { return _imgSource; }
            set
            {
                if (_imgSource != value)
                {
                    _imgSource = value;
                }
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
                    var lat = positon.Latitude.ToString("0.000");
                    var lng = positon.Longitude.ToString("0.000");

                   Application.Current.MainPage.DisplayAlert("MapClicked", $"{lat}/{lng}", "CLOSE");
                  
                });
            }
        }




        /// <summary>
        /// Handle take snapshot button event
        /// </summary>
        public Command TakeSnapshotCommand
        {
            get
            {
                return new Command(async()=>
                {
                    ImgSource = null;
                    MemoryStream stream = null;
                    stream = new MemoryStream(await DependencyService.Get<IScreenShotManager>().CaptureAsync());
                    // stream = new MemoryStream(await CrossScreenshot.Current.CaptureAsync()); // using xam.plugin.screenshot
                    ImgSource = ImageSource.FromStream(() => stream);

                });
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
                    var lat = position.Latitude.ToString("0.000");
                    var lng = position.Longitude.ToString("0.000");
                    await Application.Current.MainPage.DisplayAlert("MapClicked", $"{lat}/{lng}", "CLOSE");
                });
            }
        }

        /// <summary>
        /// The map type values bound to Picker .
        /// </summary>
        private ObservableCollection<MapType> _mapTypeValues;
        public ObservableCollection<MapType> MapTypeValues
        {
            get { return _mapTypeValues; }
            set
            {
                if (Equals(value, _mapTypeValues)) return;
                _mapTypeValues = value;
            }
        }

        /// <summary>
        /// Gets the type of the map.
        /// </summary>
        public void GetMapType()
        {
            MapTypeValues = new ObservableCollection<MapType>();

            foreach (var mapType in Enum.GetValues(typeof(MapType)))
            {
                MapTypeValues.Add((MapType)mapType);
            }
        }
    }
}
