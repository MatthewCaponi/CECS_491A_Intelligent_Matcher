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
using DataAccess.Repositories;
using Services;
using IntelligentMatcher.Services;

namespace Registration.Services
{
    public class EmailService : IEmailService
    {
		//private const string API_KEY = "7e3947d6-ad88-41aa-91ae-8166ae128b21";
		private const string API_KEY = "POSTMARK_API_TEST";
		private readonly IConfiguration _configuration;
		private const int TOKEN_LENGTH = 200;

		private readonly IUserAccountRepository _userAccountRepository;
		private readonly IAccountVerificationRepo _accountVerificationRepo;
		private readonly IUserAccountService _userAccountService;

		

		public EmailService(IUserAccountRepository userAccountRepository, IAccountVerificationRepo accountVerificationRepo, IUserAccountService userAccountService, IConfiguration configuration)
        {
			_userAccountRepository = userAccountRepository;
			_accountVerificationRepo = accountVerificationRepo;
			_userAccountService = userAccountService;
			_configuration = configuration;

		}


		private string GenerateToken()
		{


			// creating a StringBuilder object()
			StringBuilder str_build = new StringBuilder();
			Random random = new Random();

			char letter;

			for (int i = 0; i < TOKEN_LENGTH; i++)
			{
				double flt = random.NextDouble();
				int shift = Convert.ToInt32(Math.Floor(25 * flt));
				letter = Convert.ToChar(shift + 65);
				str_build.Append(letter);
			}

			string token = str_build.ToString();

			return token;

		}


		public EmailOptionsModel GetEmailOptions()
		{


			EmailOptionsModel emailOptions = new EmailOptionsModel();

			emailOptions.Sender = _configuration.GetSection("Sender").Value;
			emailOptions.MessageStream = _configuration.GetSection("MessageStream").Value;
			emailOptions.Subject = _configuration.GetSection("Subject").Value;
			emailOptions.TextBody = _configuration.GetSection("TextBody").Value;
			emailOptions.Tag = _configuration.GetSection("Tag").Value;
			emailOptions.TrackOpens = bool.Parse(_configuration.GetSection("TrackOpens").Value);
			emailOptions.HtmlBody = _configuration.GetSection("HtmlBody").Value;


			return emailOptions;

		}


		public async Task<bool> CreateVerificationToken(int userId)
        {
            try
            {
				await _accountVerificationRepo.CreateAccountVerification(userId, GenerateToken());
				return true;
            }
            catch
            {
				return false;
            }

		}

		public async Task<string> GetStatusToken(int userId)
		{
			return await _accountVerificationRepo.GetStatusTokenByUserId(userId);
		}

		public async Task DeleteIfNotActive(int userId)
		{
			string status = await _accountVerificationRepo.GetStatusTokenByUserId(userId);

			if (status != "Active")
			{
				await _userAccountService.DeleteAccount(userId);
			}

		}

		public async Task<bool> ValidateStatusToken(int userId, string token)
		{
			Console.WriteLine("Validating");
			string existingStatusToken = await _accountVerificationRepo.GetStatusTokenByUserId(userId);
			Console.WriteLine(token);
			Console.WriteLine(existingStatusToken);
			if (existingStatusToken == token)
			{


				await _userAccountRepository.UpdateAccountStatus(userId, "Active");
				await _accountVerificationRepo.UpdateAccountStatusToken(userId, GenerateToken());
				return true;
			}
			else
			{
				return false;
			}

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
