using System;
using WebApi;
using WebApi.Models;
namespace RegistrationBridge
{
    public class EmailOptions
    {
        public EmailOptionsModel GetEmailData()
        {
            EmailOptionsManager emailOptionsManager = new EmailOptionsManager();
            return emailOptionsManager.GetEmailOptions();
        }
    }
}
