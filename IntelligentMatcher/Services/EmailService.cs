using BusinessModels;
using Microsoft.Extensions.Configuration;
using PostmarkDotNet;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using WebApi.Models;

namespace Registration.Services
{
    public class EmailService : IEmailService
    {
		//private const string API_KEY = "7e3947d6-ad88-41aa-91ae-8166ae128b21";
		private const string API_KEY = "POSTMARK_API_TEST";
		private IConfigurationRoot Configuration { get; set; }



		public EmailOptionsModel GetEmailOptions()
		{

			IConfigurationBuilder builder = new ConfigurationBuilder();
			string path = Directory.GetCurrentDirectory();
			string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\"));

			builder.AddJsonFile(Path.Combine(newPath, @"WebApi\appsettings.json"));
			Configuration = builder.Build();

			EmailOptionsModel emailOptions = new EmailOptionsModel();

			emailOptions.Sender = Configuration.GetSection("Email:Sender").Value;
			emailOptions.MessageStream = Configuration.GetSection("Email:MessageStream").Value;
			emailOptions.Subject = Configuration.GetSection("Email:Subject").Value;
			emailOptions.TextBody = Configuration.GetSection("Email:TextBody").Value;
			emailOptions.Tag = Configuration.GetSection("Email:Tag").Value;
			emailOptions.TrackOpens = bool.Parse(Configuration.GetSection("Email:TrackOpens").Value);


			return emailOptions;

		}



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
				System.Threading.Thread.Sleep(500);
				if(sendResult.Status == PostmarkStatus.Success)
                {
					return true;
				}
                else
                {
					return false;
				}
			} 
			catch
            {
				return false;
			}
        }
    }
}
