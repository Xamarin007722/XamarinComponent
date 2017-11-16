using System;
using Xamarin.Forms;

namespace XamarinComponent.Behaviors
{
    public class DateValidationBehavior:Behavior<DatePicker>
    {
        protected override void OnAttachedTo(DatePicker bindable)  
        {  
            bindable.DateSelected += Datepicker_DateSelected;  
            base.OnAttachedTo(bindable);  
        }  
  
        private void Datepicker_DateSelected(object sender, DateChangedEventArgs e)  
        {  
            DateTime value = e.NewDate;  
            int year = DateTime.Now.Year;  
            int selyear = value.Year;  
            int result = selyear - year;  
            bool isValid=false;  
            if(result <=100 && result >0)  
            {  
                isValid = true;  
            }  
           ((DatePicker)sender).BackgroundColor = isValid ? Color.Default : Color.Red;  
        }  
  
        protected override void OnDetachingFrom(DatePicker bindable)  
        {  
            bindable.DateSelected -= Datepicker_DateSelected;  
            base.OnDetachingFrom(bindable);  
        }  
  
    }
}
