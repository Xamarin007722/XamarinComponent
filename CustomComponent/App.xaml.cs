using System;
using System.Collections.Generic;
using CustomComponent.CustomView;
using CustomComponent.TKCustomMAP.Pages;
using CustomComponent.Views;
using TK.CustomMap;
using TK.CustomMap.Api.Google;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinComponent.Views;

namespace CustomComponent
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            TK.CustomMap.Api.GoogleAPI.GmsPlace.Init("AIzaSyCJN3Cd-Sp1a5V5OnkvTR-Gqhx7A3S-b6M");
            GmsDirection.Init("AIzaSyCJN3Cd-Sp1a5V5OnkvTR-Gqhx7A3S-b6M");
            //MainPage = new MapView();
            // The root page of your application
            MyCustomMap map = new MyCustomMap();
            map.MapType = MapType.Street;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(17.4474, 78.3762), Distance.FromKilometers(8)));
            map.CustomPins = new List<TKCustomMapPin>(new[]
                            {

                                new TKCustomMapPin
                                {
                                    Title = "Custom Callout Sample",
                                    Position = new Position(17.4474, 78.3762),
                                    ShowCallout = true,
                                    IsDraggable=true
                             }
            });

          
            Entry searchPosition = new Entry()
            {
                Placeholder ="Search Places",
                WidthRequest =250,
                HeightRequest =35,
                TextColor=Color.Red
            };
            Button btnSearch = new Button()
            {
                Text="Search Now",
                TextColor=Color.White,
                BackgroundColor=Color.Black,
                FontSize=12
            };
            StackLayout stkSearch = new StackLayout()
            {
                Orientation=StackOrientation.Horizontal,
                Margin=new Thickness(5,10,5,5)
            };
            stkSearch.Children.Add(searchPosition);
            stkSearch.Children.Add(btnSearch);
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        Children =
            //        {
            //            stkSearch,
            //            map
            //        }
            //    }
            //};
            SearchNearBy();
            MainPage = new NavigationPage(new SamplePage());
        }
        async void SearchNearBy()
        {
            var result = await TK.CustomMap.Api.GoogleAPI.GmsPlace.Instance.GetNearByPredictions("school");
        }

    }
}
