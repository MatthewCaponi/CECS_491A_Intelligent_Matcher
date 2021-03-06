using BusinessModels;
using PostmarkDotNet;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace Registration.Services
{
    public class EmailService : IEmailService
    {
		private const string API_KEY = "7e3947d6-ad88-41aa-91ae-8166ae128b21";
        public async Task<bool> SendEmail(EmailModel emailModel)
        {
			var message = new PostmarkMessage()
			{
				To = emailModel.Recipient,
				From = emailModel.Sender,
				TrackOpens = emailModel.TrackOpens,
				Subject = emailModel.Subject,
				TextBody = emailModel.TextBody,
				HtmlBody = emailModel.HtmlBody,
				MessageStream = emailModel.MessageStream,
				Tag = emailModel.Tag
			};

			

			var client = new PostmarkClient(API_KEY);			
            try
            {
				var sendResult = await client.SendMessageAsync(message);
				return true;
			} 
			catch
            {
				return false;
			}
        }
    }
}
