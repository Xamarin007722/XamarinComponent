using System;
using System.Collections.Generic;
using System.Linq;
using CustomComponent;
using Foundation;
using Plugin.Toasts;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

namespace XamarinComponent.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            DependencyService.Register<ToastNotification>();
            Xamarin.FormsGoogleMaps.Init("AIzaSyD3YCjiRwumZvVbAKIpoc1Pu2ZSdFJRtPw");
            ToastNotification.Init();

            LoadApplication(new App());

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Request Permissions
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (granted, error) =>
                {
                    // Do something if needed
                });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                 UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                    );

                app.RegisterUserNotificationSettings(notificationSettings);
            }

            //var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            //x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            //x = typeof(Xamarin.Forms.Themes.iOS.UnderlineEffect);
            return base.FinishedLaunching(app, options);
        }

        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {

            // Local Notifications are received here
        }
    }
}
