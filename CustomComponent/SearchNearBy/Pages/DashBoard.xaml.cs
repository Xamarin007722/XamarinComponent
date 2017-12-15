using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using CustomComponent.SearchNearBy.ViewModels;
using ProgressRingControl.Forms.Plugin;
using Acr.UserDialogs;

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
            mapView.SetBinding(TKCustomMap.RouteCalculationFinishedCommandProperty, "RouteCalculationFinishedCommand");
            mapView.IsShowingUser = true;
            stkMap.Children.Add(mapView);
            this.BindingContext = new DashBoardViewModel();
        }

       protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();  
        }
    }
}
