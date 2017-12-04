using System;
using Android.Widget;
using CustomComponent.CustomView;
using TK.CustomMap.Droid;
using Xamarin.Forms;
using XamarinComponent.Droid.InterfaceImplementation;

[assembly: ExportRenderer(typeof(MyCustomMap), typeof(MyMapRenderer))]
namespace XamarinComponent.Droid.InterfaceImplementation
{
    public class MyMapRenderer: TKCustomMapRenderer, Android.Gms.Maps.GoogleMap.IInfoWindowAdapter
    {
        protected override void OnMapReady(Android.Gms.Maps.GoogleMap googleMap)
        {
            base.OnMapReady(googleMap);

            googleMap.SetInfoWindowAdapter(this);
        }

        Android.Views.View Android.Gms.Maps.GoogleMap.IInfoWindowAdapter.GetInfoContents(Android.Gms.Maps.Model.Marker marker)
        {
            var pin = this.GetPinByMarker(marker);
            if (pin == null) return null;

            var image = new ImageView(this.Context);
            image.SetImageResource(Resource.Drawable.mapIcon);

            return image;
        }

        Android.Views.View Android.Gms.Maps.GoogleMap.IInfoWindowAdapter.GetInfoWindow(Android.Gms.Maps.Model.Marker marker)
        {
            return null;
        }
    }
}
