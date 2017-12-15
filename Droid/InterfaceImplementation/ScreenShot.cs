using System;
using System.IO;
using Android.App;
using Android.Graphics;
using CustomComponent.Interface;
using Xamarin.Forms;
using XamarinComponent.Droid.InterfaceImplementation;

[assembly:Dependency(typeof(ScreenShot))]
namespace XamarinComponent.Droid.InterfaceImplementation
{
    public class ScreenShot:IScreenShotManager
    {
        //public static Activity Activity { get; set; }
     
        public async System.Threading.Tasks.Task<byte[]> CaptureAsync()
        {
            var activity = Forms.Context as Activity;
            if (activity == null)
            {
                throw new Exception("You have to set ScreenshotManager.Activity in your Android project");
            }
            var view = ((Activity)Xamarin.Forms.Forms.Context).Window.DecorView;
            view.DrawingCacheEnabled = true;

            Bitmap bitmap = view.GetDrawingCache(true);

            byte[] bitmapData;

            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }
            return bitmapData;
        }
    
    }
}
