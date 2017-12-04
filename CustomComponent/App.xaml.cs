using System;
using System.Collections.Generic;
using CustomComponent.CustomView;
using CustomComponent.Views;
using TK.CustomMap;
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
           // MainPage = new MapView();
            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new MyCustomMap
                        {
                            MapRegion = MapSpan.FromCenterAndRadius(new Position(17.4474, 78.3762), Distance.FromKilometers(1)),
                            CustomPins = new List<TKCustomMapPin>(new[]
                            {
                                new TKCustomMapPin
                                {
                                    Title = "Custom Callout Sample",
                                    Position = new Position(17.4474, 78.3762),
                                    ShowCallout = true
                                }
                            })
                        }
                    }
                }
            };
        }
    }
}
