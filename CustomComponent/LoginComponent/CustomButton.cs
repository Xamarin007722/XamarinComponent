using System;
using Xamarin.Forms;

namespace CustomComponent.LoginComponent
{
    public class CustomButton:Button
    {
        internal CustomButton(string text)
        {
            BackgroundColor = Color.Red;
            Text = text;
            TextColor = Color.Black;
        }
    }
}
