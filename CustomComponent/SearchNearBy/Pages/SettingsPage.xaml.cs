using System;
using System.Collections.Generic;
using CustomComponent.SearchNearBy.ViewModels;
using Xamarin.Forms;

namespace CustomComponent.SearchNearBy.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.BindingContext = new SettingsViewModel();
        }
    }
}
