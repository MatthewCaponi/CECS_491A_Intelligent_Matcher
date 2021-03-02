using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Models.UserProfileModel;

namespace Registration
{
    public class EmailVerificationService
    {
        public async Task<bool> SendEmail(webUserAccountModel accountModel)
        {
            return true;
        }
    }
}
