// <summary>
// Password Validation behaviors
// Created by Akhilesh 7 Nov 2017
// </summary>
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace XamarinComponent.Behaviors
{
    public class PasswordValidationBehavior:Behavior<Entry>
    {
        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";  
  
  
        protected override void OnAttachedTo(Entry bindable)  
        {  
            bindable.TextChanged += HandleTextChanged;  
            base.OnAttachedTo(bindable);  
        }  
  
        void HandleTextChanged(object sender, TextChangedEventArgs e)  
        {  
            bool IsValid = false;  
            IsValid = (Regex.IsMatch(e.NewTextValue, passwordRegex));  
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;  
        }  
  
        protected override void OnDetachingFrom(Entry bindable)  
        {  
            bindable.TextChanged -= HandleTextChanged;  
            base.OnDetachingFrom(bindable);  
        }  
    }
}
