using PostmarkDotNet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace Registration.Services
{
    public class EmailVerificationService
    {
        public async Task<bool> SendVerificationEmail(WebUserAccountModel accountModel)
        {
			var message = new PostmarkMessage()
			{
				To = accountModel.EmailAddress,
				From = "support@infinimuse.com",
				TrackOpens = true,
				Subject = "Verify Your Email",
				TextBody = "You have Successfully Registered! Click on the link to verify your email!",
				HtmlBody = "<a href='index.cshtml'>Verify Email</a> <strong>Link Expires 24 hours after Registering.</strong>",
				MessageStream = "outbound",
				Tag = "Verifcation"
			};

			var client = new PostmarkClient("7e3947d6-ad88-41aa-91ae-8166ae128b21");
			var sendResult = await client.SendMessageAsync(message);

			if (sendResult.Status == PostmarkStatus.Success)
			{
				return true;
			}
			else
			{
				return false;
			}
        }
    }
}
