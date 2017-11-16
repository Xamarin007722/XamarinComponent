using System;
using System.Linq;
using Xamarin.Forms;

namespace XamarinComponent.Behaviors
{
    public class ConsumeBehaviorWithStyle:Behavior<Entry>
    {
        public static readonly BindableProperty AttachBehaviorProperty =
            BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(ConsumeBehaviorWithStyle), false, propertyChanged: OnAttachBehaviorChanged);

        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            var entry = bindable as Entry;
            var toRemove = entry.Behaviors.FirstOrDefault(b => b is ConsumeBehaviorWithStyle);
            if (toRemove != null)
            {
                entry.Behaviors.Remove(toRemove);
            }
            //entry.Behaviors.Clear();
        }

        static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
        {
            var entry = view as Entry;
            if (entry == null)
            {
                return;
            }

            bool attachBehavior = (bool)newValue;
            if (attachBehavior)
            {
                entry.Behaviors.Add(new ConsumeBehaviorWithStyle());
            }
            else
            {
                var toRemove = entry.Behaviors.FirstOrDefault(b => b is ConsumeBehaviorWithStyle);
                if (toRemove != null)
                {
                    entry.Behaviors.Remove(toRemove);
                }
            }
        }
    }
}
//Consume

//<Style x:Key="NumericValidationStyle" TargetType="Entry">
//    <Style.Setters>
//        <Setter Property = "local:NumericValidationBehavior.AttachBehavior" Value="true" />
//    </Style.Setters>
//</Style>

 //<Entry Placeholder = "Enter a System.Double" Style="{StaticResource NumericValidationStyle}">