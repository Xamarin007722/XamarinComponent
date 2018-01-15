using System;
using Plugin.Messaging;
using Xamarin.Forms;

namespace CustomComponent.SearchNearBy.ViewModels
{
    /// <summary>
    /// Image large view model.
    /// </summary>
    public class ImageLargeViewModel
    {
        public ImageLargeViewModel()
        {
        }

        /// <summary>
        /// Gets the make call API to initiate the call.
        /// </summary>
        /// <value>The make call command.</value>
        public Command MakeCallCommand
        {
            get { return new Command(()=>
            {
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall)
                    phoneDialer.MakePhoneCall("+919990199666");

            }); }

        }
        /// <summary>
        /// Gets the write email API to send a mail.
        /// </summary>
        /// <value>The write email command.</value>
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
        /// <summary>
        /// Gets the do chat API to send a message.
        /// </summary>
        /// <value>The do chat command.</value>
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
