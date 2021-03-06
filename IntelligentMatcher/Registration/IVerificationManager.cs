using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Registration
{
    public interface IVerificationManager
    {
        Task<bool> LinkExpired(int accountId);
        Task<bool> VerifyEmail(int accountId);
    }
}
