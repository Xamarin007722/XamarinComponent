using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Plugin.Toasts;
using Android.OS;
using Xamarin.Forms;
using CustomComponent;

namespace XamarinComponent.Droid
{
    [Activity(Label = "XamarinComponent.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            //var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            //x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            //x = typeof(Xamarin.Forms.Themes.Android.UnderlineEffect);
            DependencyService.Register<ToastNotification>();
            ToastNotification.Init(this, new PlatformOptions()
            {
                SmallIconDrawable = Android.Resource.Drawable.IcDialogAlert,
                Style = NotificationStyle.Snackbar
            });
            //FormsMap.Init(this, savedInstanceState);
            LoadApplication(new App());

        }
    }
}
