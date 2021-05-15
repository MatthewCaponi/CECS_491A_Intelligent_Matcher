using Microsoft.Extensions.Configuration;
using BusinessModels;
using Services;
using System;
using System.Threading.Tasks;

namespace Help
{
    public class HelpManager : IHelpManager
    {
        private const string helpTag = "Help";
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public HelpManager(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<bool> SendHelpEmail(string subject, string message)
        {
            bool IsSuccessful = false;
            if(subject == null || message == null)
            {
                return IsSuccessful;
            }

            EmailModel emailModel = new EmailModel();

            emailModel.Sender = _configuration["Sender"];
            emailModel.Recipient = _configuration["Sender"];
            emailModel.TrackOpens = true;
            emailModel.Subject = "[" + _configuration["Sender"] + "]: " + subject;
            emailModel.TextBody = message;
            emailModel.HtmlBody = message;
            emailModel.MessageStream = _configuration["MessageStream"];
            emailModel.Tag = helpTag;

            IsSuccessful = await _emailService.SendEmail(emailModel);

            return IsSuccessful;
        }
    }
}
