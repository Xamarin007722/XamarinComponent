using System;
using System.Collections.Generic;
using CustomComponent.SearchNearBy.ViewModels;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CustomComponent.SearchNearBy.Pages
{
    public partial class DemoPage : ContentPage
    {
        public DemoPage()
        {
            InitializeComponent();
            INIT();
            this.BindingContext = new DemoViewModel();
        }

        public void INIT()
        {
           
            var HitechCity = new Position(17.4474, 78.3762);
            var mapView = new TKCustomMap(MapSpan.FromCenterAndRadius(HitechCity, Distance.FromKilometers(2)));
            mapView.SetBinding(TKCustomMap.CustomPinsProperty, "Pins");
            mapView.SetBinding(TKCustomMap.MapClickedCommandProperty, "MapClickedCommand");
            mapView.SetBinding(TKCustomMap.MapLongPressCommandProperty, "MapLongPressCommand");

            mapView.SetBinding(TKCustomMap.PinSelectedCommandProperty, "PinSelectedCommand");
            //mapView.SetBinding(TKCustomMap.SelectedPinProperty, "SelectedPin");
            mapView.SetBinding(TKCustomMap.RoutesProperty, "Routes");
            //mapView.SetBinding(TKCustomMap.CirclesProperty, "Circles");
            //mapView.SetBinding(TKCustomMap.CalloutClickedCommandProperty, "CalloutClickedCommand");
            mapView.SetBinding(TKCustomMap.PolylinesProperty, "Lines");
            //mapView.SetBinding(TKCustomMap.PolygonsProperty, "Polygons");
            mapView.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
            mapView.SetBinding(TKCustomMap.RouteCalculationFinishedCommandProperty, "RouteCalculationFinishedCommand");
            //mapView.IsRegionChangeAnimated = true;

            mapView.IsShowingUser = true;

            stkMap.Children.Add(mapView);
        }
    }
}
