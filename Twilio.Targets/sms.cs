using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace Twilio.Targets
{
     [NLog.Targets.Target("Sms")]
    public class Sms :NLog.Targets.TargetWithLayout
    {
        [Required]
        public string AccountSid { get; set; }

        [Required]
        public string AuthToken { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        protected override void Write(NLog.LogEventInfo logEvent)
        {
            string logMessage = this.Layout.Render(logEvent);

            SendTheMessageToTheRemoteHost(logMessage);
        }

        private void SendTheMessageToTheRemoteHost(string message)
        {
            string msg = (message.Length > 160) ? message.Substring(0, 160) : message;

            //var client = new TwilioRestClient(this.AccountSid, this.AuthToken);
       
            //client.SendSmsMessage(
            //    this.From,
            //    this.To,
            //    msg
            //);

            TwilioClient.Init(this.AccountSid, this.AuthToken);
            var message1 = MessageResource.Create(
               to: new PhoneNumber(this.To),
               from: new PhoneNumber(this.From),
               body: msg);
        }

    }
}
