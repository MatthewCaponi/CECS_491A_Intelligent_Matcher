using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IEmailVerificationService
    {
        Task<bool> SendVerificationEmail(string emailAddress);
    }
}
