using System;
using MvvmHelpers;
using Xamarin.Forms;

namespace CustomComponent.SearchNearBy.Models
{
    public class DashboardModel:BaseViewModel
    {
        string srchbtnone;
        public string SrchBtnOne
        {
            get => srchbtnone;
            set => SetProperty(ref srchbtnone, value);
        }

        string srchbtntwo;
        public string SrchBtnTwo
        {
            get => srchbtntwo;
            set => SetProperty(ref srchbtntwo, value);
        }

        string srchbtnthree;
        public string SrchBtnThree
        {
            get => srchbtnthree;
            set => SetProperty(ref srchbtnthree, value);
        }

        string srchbtnfour;
        public string SrchBtnFour
        {
            get => srchbtnfour;
            set => SetProperty(ref srchbtnfour, value);
        }
        string lat;
        public string Lat
        {
            get => lat;
            set => SetProperty(ref lat, value);
        }
        string longi;
        public string Long
        {
            get => longi;
            set => SetProperty(ref longi, value);
        }

        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        string rating;
        public string Rating
        {
            get => rating;
            set => SetProperty(ref rating, value);
        }
        string address;
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }
        string status;
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }
        ImageSource imgsource;
        public ImageSource ImgSource
        {
            get => imgsource;
            set => SetProperty(ref imgsource, value);
        }
    }
}
