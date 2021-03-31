﻿using BusinessModels;
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
		private const string API_KEY = "POSTMARK_API_TEST";
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
