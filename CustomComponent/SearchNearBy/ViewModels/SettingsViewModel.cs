using System;
using CustomComponent.SearchNearBy.Models;
using Xamarin.Forms;
using XamarinComponent.Interface;

namespace CustomComponent.SearchNearBy.ViewModels
{
    public class SettingsViewModel:SettingsModel
    {
        public SettingsViewModel()
        {
            
        }

        /// <summary>
        /// Save the record into the local database.
        /// </summary>
        /// <value>The save command.</value>
        public Command SaveCommand
        {
            get { return new Command(()=>
            {
               try
                {
                    SettingsDataModel model = new SettingsDataModel();
                    model.FirstBtnText = EntryFirstText;
                    model.SecondBtnText = EntrySecondText;
                    model.ThirdBtnText = EntryThirdText;
                    model.FourthBtnText = EntryFourthText;

                    model.FirstSearchText = EntryFirstSearchText;
                    model.SecondSearchText = EntrySecondSearchText;
                    model.ThirdSearchText = EntryThirdSearchText;
                    model.FourthSearchText = EntryFourthSearchText;

                    App.SettingDatabase.SaveSettings(model);
                    model.Dispose();
                    ClearData();
                    DependencyService.Get<IAndroidPopUp>().ShowSnackbar("Settings saved successfully");
                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Something Went Wrong", "OK");
                         
                }
                finally 
                {
                    

                }

            }); }
        }

        protected void ClearData()
        {
            EntryFirstText = string.Empty;
            EntrySecondText = string.Empty;
            EntryThirdText = string.Empty;
            EntryFourthText = string.Empty;

            EntryFirstSearchText = string.Empty;
            EntrySecondSearchText = string.Empty;
            EntryThirdSearchText = string.Empty;
            EntryFourthSearchText = string.Empty;
        }

    }
}
