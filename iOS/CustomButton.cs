using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinComponent.iOS;

[assembly: ExportRenderer(typeof(Button),typeof(CustomButton))]
namespace XamarinComponent.iOS
{
    public class CustomButton:ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control!=null)
            {
                    Control.BackgroundColor = UIColor.Red;
                    Control.TintColor = UIColor
                        .White;
                Control.SetTitleColor(UIColor.White,UIControlState.Normal);
            }
        }

    }
}
