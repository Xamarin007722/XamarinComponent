using CustomComponent.SearchNearBy.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace CustomComponent.SearchNearBy.Pages
{
    public partial class ImageLargeView : PopupPage
    {
        ImageSource _imagesource;
        public ImageLargeView(ImageSource source)
        {
            InitializeComponent();
            _imagesource = source;
            largeImage.Source = _imagesource;
            this.BindingContext = new ImageLargeViewModel();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }
        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
    }

}
