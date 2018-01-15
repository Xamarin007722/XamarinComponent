using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using CustomComponent.SearchNearBy.ViewModels;

namespace CustomComponent.SearchNearBy.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashBoard : ContentPage
    {
        TKCustomMap mapView = null;

        public DashBoard()
        {
            InitializeComponent();
            Title = "Find Nearby ";
            mapView = new TKCustomMap(MapSpan.FromCenterAndRadius(new Position(17.4354, 78.3827), Distance.FromKilometers(1)));
            mapView.SetBinding(TKCustomMap.PinSelectedCommandProperty, "PinSelectedCommand");
            mapView.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
            mapView.SetBinding(TKCustomMap.CustomPinsProperty, "CustomPins");
            //mapView.SetBinding(TKCustomMap.CustomPinsProperty, "Pins");
            mapView.SetBinding(TKCustomMap.RoutesProperty, "Routes");
            //mapView.SetBinding(TKCustomMap.PolylinesProperty,"Polylines");
            //mapView.SetBinding(TKCustomMap.PolylinesProperty, "Lines");
            //mapView.SetBinding(TKCustomMap.CustomPinsProperty, "Pins");
            mapView.SetBinding(TKCustomMap.RouteCalculationFinishedCommandProperty, "RouteCalculationFinishedCommand");
            mapView.IsShowingUser = true;
            stkMap.Children.Add(mapView);
            var DbVM=new DashBoardViewModel();
            //lstOfResults.IsPullToRefreshEnabled = true;
            //lstOfResults.SetBinding(ListView.IsRefreshingProperty,nameof(DbVM.IsBusy));
            //lstOfResults.RefreshCommand = DbVM.ListRefreshCommand;
            this.BindingContext = DbVM;
        }

        //async void  OnTapGestureRecognizerTapped(object sender, EventArgs args)
        //{
        //    var model= sender as DashboardModel;
        //    await Navigation.PushPopupAsync(new ImageLargeView(model.ImgSource));
        //}

       protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();  
        }
    }
}
