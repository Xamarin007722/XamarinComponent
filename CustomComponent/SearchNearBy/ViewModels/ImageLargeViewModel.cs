using System;
using Plugin.Messaging;
using Xamarin.Forms;

namespace CustomComponent.SearchNearBy.ViewModels
{
    public class ImageLargeViewModel
    {
        public ImageLargeViewModel()
        {
        }

        public Command MakeCallCommand
        {
            get { return new Command(()=>
            {
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall)
                    phoneDialer.MakePhoneCall("+919990199666");

            }); }

        }
        public Command WriteEmailCommand
        {
            get
            {
                return new Command(() =>
                {
                    var emailMessenger = CrossMessaging.Current.EmailMessenger;
                    if (emailMessenger.CanSendEmail)
                    {
                        // Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc. 
                        var email = new EmailMessageBuilder()
                            .To("Xamarin4forms@gmail.com")
                            .Subject("")
                            .Body("")
                          .Build();

                        emailMessenger.SendEmail(email);
                    }

                });
            }

        }
        public Command DoChatCommand
        {
            get
            {
                return new Command(() =>
                {
                    var smsMessenger = CrossMessaging.Current.SmsMessenger;
                    if (smsMessenger.CanSendSms)
                        smsMessenger.SendSms("+919990199666","");
                });
            }

        }
    }
}
