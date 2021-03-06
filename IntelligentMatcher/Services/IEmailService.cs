using BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailModel emailModel);
    }
}
