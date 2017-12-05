using System;
using System.Collections.Generic;
using CustomComponent.TKCustomMAP.ViewModels;
using TK.CustomMap.Overlays;
using Xamarin.Forms;

namespace CustomComponent.TKCustomMAP.Pages
{
    public partial class HTMLInstructionsPage : ContentPage
    {
        public HTMLInstructionsPage(TKRoute route)
        {
            InitializeComponent();
            BindingContext = new HtmlInstructionsViewModel(route);
        }
    }
}
