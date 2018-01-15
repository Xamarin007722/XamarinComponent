using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomComponent.UserControl
{
    public partial class CustomLoador : Grid
    {
        /// <summary>
        /// InfoMessage bindable property.
        /// </summary>
        private static BindableProperty InfoMessage = BindableProperty.Create(
                                                         propertyName: "InfoText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomLoador),
                                                         defaultValue: "Loading...",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: titleTextPropertyChanged);
        /// <summary>
        /// InfoIcon bindable property.
        /// </summary>
        private static BindableProperty InfoIcon = BindableProperty.Create(
                                                       propertyName: "InfoImage",
                                                       returnType: typeof(string),
                                                       declaringType: typeof(CustomLoador),
                                                       defaultValue: "",
                                                       defaultBindingMode: BindingMode.TwoWay,
                                                       propertyChanged: ImageSourcePropertyChanged);

        public CustomLoador()
        {
            InitializeComponent();
            Animate();
        }
        /// <summary>
        /// Rotate the image
        /// </summary>
        public void Animate()
        {
            imgIcon.RotateTo(307 * 360, 600000);
        }

        public string InfoText
        {
            get { return (string)GetValue(InfoMessage); }
            set { SetValue(InfoMessage, value); }
        }

        public string InfoImage
        {
            get { return (string)GetValue(InfoIcon); }
            set { SetValue(InfoIcon, value); }
        }

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomLoador)bindable;
            control.imgIcon.Source = ImageSource.FromFile(newValue.ToString());
        }

        private static void titleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomLoador)bindable;
            control.lblMsg.Text = newValue.ToString();
        }
    }
}
