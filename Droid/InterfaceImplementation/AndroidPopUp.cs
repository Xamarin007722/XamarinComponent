using System;
using Android.App;
using Android.Support.Design.Widget;
using Android.Widget;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using XamarinComponent.Interface;

[assembly: Dependency(typeof(XamarinComponent.Droid.InterfaceImplementation.AndroidPopUp))]
namespace XamarinComponent.Droid.InterfaceImplementation
{
    public class AndroidPopUp:IAndroidPopUp
    {
        public void ShowSnackbar(string message)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            // Get root view of current activity
            Android.Views.View activityRootView = activity.FindViewById(Android.Resource.Id.Content);
            Snackbar.Make(activityRootView, message, Snackbar.LengthLong).SetAction("Ok",(View)=>{}) .Show();
        }

        public void ShowToast(string message)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            Toast.MakeText(Forms.Context, message, ToastLength.Long).Show();
        }
    }
}
