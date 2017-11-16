using System;
using Xamarin.Forms;
namespace CustomComponent.UserControl
{
    public class MyActivityIndicator:ActivityIndicator
    {

        //Bindable property for the progress color
       
        public MyActivityIndicator()
        {
            Color = Color.Red;
        }
    }
}
