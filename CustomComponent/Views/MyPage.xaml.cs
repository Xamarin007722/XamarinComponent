using System;
using System.Collections.Generic;
using Plugin.Toasts;
using Xamarin.Forms;
using XamarinComponent.Interface;

namespace XamarinComponent.Views
{
    public partial class MyPage :ContentPage
    {
        public MyPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Shows the toast message.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void ShowToast(object sender, System.EventArgs e)
        {
            DependencyService.Get<IAndroidPopUp>().ShowToast("Hi I'm Toast");
        }
        /// <summary>
        /// Shows the message as snack bar.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void ShowSnackBar(object sender, System.EventArgs e)
        {
            DependencyService.Get<IAndroidPopUp>().ShowSnackbar("Hi I'm Snack Bar");
        }

        public void LoadingOverLay()
        {
            
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var options = new NotificationOptions()
                {
                    Title = "Title",
                    Description = "Some Description",
                    IsClickable = false,
                    ClearFromHistory = true,
                    DelayUntil = DateTime.Now.AddSeconds(1)
                };
                var notificator = DependencyService.Get<IToastNotificator>();
                var result = notificator.Notify(options);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
