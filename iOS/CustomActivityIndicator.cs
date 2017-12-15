using System;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using CustomComponent.UserControl;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinComponent.iOS;

[assembly: ExportRenderer(typeof(MyActivityIndicator), typeof(CustomActivityIndicator))]
namespace XamarinComponent.iOS
{
    public class CustomActivityIndicator : ViewRenderer<ActivityIndicator, UIActivityIndicatorView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ActivityIndicator> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
                    activityIndicator.Frame = new CGRect((this.Bounds.Width / 2) - (activityIndicator.Frame.Width / 2)
                        , 50, activityIndicator.Frame.Width, activityIndicator.Frame.Height);

                    SetNativeControl
                    (
                        activityIndicator);
                        //new UIActivityIndicatorView(new CGRect(30.0, 75.0, 200.0, 80.0))
                        //{
                        //    ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.WhiteLarge
                        //});

                }
                UpdateColor();
                UpdateIsRunning();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ActivityIndicator.ColorProperty.PropertyName)
                UpdateColor();
            else if (e.PropertyName == ActivityIndicator.IsRunningProperty.PropertyName)
                UpdateIsRunning();
        }
        void UpdateColor()
        {
            Control.Color = Element.Color == Xamarin.Forms.Color.Default ? null : Xamarin.Forms.Color.Green.ToUIColor();
        }
        void UpdateIsRunning()
        {
            if (Element.IsRunning)
                Control.StartAnimating();
            else
                Control.StopAnimating();
        }
        internal void PreserveState()
        {
            if (Control != null && !Control.IsAnimating && Element != null && Element.IsRunning)
                Control.StartAnimating();
        }
    }
}

