using BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailModel emailModel);

        EmailOptionsModel GetEmailOptions();

        Task<string> GetStatusToken(int userId);

        Task DeleteIfNotActive(int userId);

        Task<bool> CreateVerificationToken(int userId);


        Task<bool>  ValidateStatusToken(int userId, string token);

    }
}
