using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Registration
{
    public interface IVerificationManager
    {
        Task<bool> LinkExpired(int AccountId);
        Task<bool> VerifyEmail(int AccountId);
    }
}
