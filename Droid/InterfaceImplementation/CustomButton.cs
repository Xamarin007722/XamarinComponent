using System;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinComponent.Droid.InterfaceImplementation;

[assembly: ExportRenderer(typeof(Button), typeof(CustomButton))]
namespace XamarinComponent.Droid.InterfaceImplementation
{
    public class CustomButton : ButtonRenderer
    {
        public CustomButton(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Button>  e)
        {
            base.OnElementChanged(e);
            var btn = this.Control as Android.Widget.Button;
            btn?.SetBackgroundResource(Resource.Drawable.Style);
        }
    }
}
