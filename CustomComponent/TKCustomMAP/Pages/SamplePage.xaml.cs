using System;
using CustomComponent.TKCustomMAP.ViewModels;
using FAB.Forms;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CustomComponent.TKCustomMAP.Pages
{
    public partial class SamplePage : ContentPage
    {
        public SamplePage()
        {
            InitializeComponent();
             
            CreateView();
            BindingContext = new SampleViewModel();
        }

        async void CreateView()
        {
            var autoComplete = new PlacesAutoComplete { ApiToUse = PlacesAutoComplete.PlacesApi.Google };
            autoComplete.SetBinding(PlacesAutoComplete.PlaceSelectedCommandProperty, "PlaceSelectedCommand");

            var HitechCity = new Position(17.4474, 78.3762);
            var mapView = new TKCustomMap(MapSpan.FromCenterAndRadius(HitechCity, Distance.FromKilometers(2)));
            mapView.SetBinding(TKCustomMap.CustomPinsProperty, "Pins");
            mapView.SetBinding(TKCustomMap.MapClickedCommandProperty, "MapClickedCommand");
            mapView.SetBinding(TKCustomMap.MapLongPressCommandProperty, "MapLongPressCommand");

            mapView.SetBinding(TKCustomMap.PinSelectedCommandProperty, "PinSelectedCommand");
            mapView.SetBinding(TKCustomMap.SelectedPinProperty, "SelectedPin");
            mapView.SetBinding(TKCustomMap.RoutesProperty, "Routes");
            mapView.SetBinding(TKCustomMap.CirclesProperty, "Circles");
            mapView.SetBinding(TKCustomMap.CalloutClickedCommandProperty, "CalloutClickedCommand");
            mapView.SetBinding(TKCustomMap.PolylinesProperty, "Lines");
            mapView.SetBinding(TKCustomMap.PolygonsProperty, "Polygons");
            mapView.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
            mapView.SetBinding(TKCustomMap.RouteCalculationFinishedCommandProperty, "RouteCalculationFinishedCommand");
            mapView.IsRegionChangeAnimated = true;

            autoComplete.SetBinding(PlacesAutoComplete.BoundsProperty, "MapRegion");

            //Content = mapView;

            //FloatingActionButton normalFab = new FloatingActionButton();
            //normalFab.Source = "plus.png";
            //normalFab.Size = FabSize.Normal;
            //normalFab.RippleColor = Color.Gray;

            _baseLayout.Children.Add(
                mapView,
                Constraint.RelativeToView(autoComplete, (r, v) => v.X),
                Constraint.RelativeToView(autoComplete, (r, v) => autoComplete.HeightOfSearchBar),
                heightConstraint: Constraint.RelativeToParent((r) => r.Height - autoComplete.HeightOfSearchBar),
                widthConstraint: Constraint.RelativeToView(autoComplete, (r, v) => v.Width));

            _baseLayout.Children.Add(
                autoComplete,
                Constraint.Constant(0),
                Constraint.Constant(0));

            //_baseLayout.Children.Add(
            //    normalFab,
            //    xConstraint: Constraint.RelativeToParent((parent) => { return (parent.Width - normalFab.Width) - 16; }),
            //    yConstraint: Constraint.RelativeToParent((parent) => { return (parent.Height - normalFab.Height) - 16; })
            //);
            //normalFab.SizeChanged += (sender, args) => { _baseLayout.ForceLayout(); };
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}