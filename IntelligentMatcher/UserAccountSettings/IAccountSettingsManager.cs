﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace UserAccountSettings
{
    public interface IAccountSettingsManager
    {
        Task<bool> CreateUserAccountSettings(UserAccountSettingsModel model);

        Task<string> ChangePassword(string oldPassword, string newPassword, int UserID);
        Task<string> ChangeEmail(string oldPassword, string email, int UserID);
        Task<string> DeleteAccountByUserIDAsync(int UserID, string password);
        Task<bool> ChangeFontSize(int UserID, int FontSize);

        Task<bool> ChangeThemeColor(int UserID, string ThemeColor);  
        


        Task<bool> ChangeFontStyleAsync(int UserID, string FontStyle);



    }
}
