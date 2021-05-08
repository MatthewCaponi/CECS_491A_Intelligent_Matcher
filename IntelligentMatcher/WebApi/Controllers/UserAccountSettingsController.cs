using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Security;
using DataAccess;
using DataAccess.Repositories;

using UserAccountSettings;

namespace IntelligentMatcherUI.Controllers
{
    public class DeleteModel
    {
        public int id { get; set; }
        public string password { get; set; }

    }
    public class FontSizeModel
    {
        public int id { get; set; }
        public string fontSize { get; set; }


    }



    public class FontStyleModel
    {
        public int id { get; set; }
        public string fontStyle { get; set; }


    }


    public class ThemeModel
    {
        public int id { get; set; }
        public string theme { get; set; }


    }
    public class PasswordModel
    {
        public int id { get; set; }
        public string newPassword { get; set; }

        public string oldPassword { get; set; }


    }
    public class EmailModel
    {
        public int id { get; set; }
        public string email { get; set; }

        public string password { get; set; }


    }
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserAccountSettingsController : ControllerBase
    {



        /*
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }*/


        [HttpPost("delete")]
        public async Task<bool> DeleteAccountAsync([FromBody] DeleteModel deleteModel)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IPasswordValidationService authenticationService = new PasswordValidationService(userAccountRepository);
            IAccountSettingsService userAccountSettingsManager = new AccountSettingsService(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            bool result = await userAccountSettingsManager.DeleteAccountByUserIDAsync(deleteModel.id, deleteModel.password);

            Console.WriteLine("Account Deleted");
            return result;
        }
        [HttpPost("changepassword")]
        public async Task<bool> PasswordChangeAsync([FromBody] PasswordModel passwordModel)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IPasswordValidationService authenticationService = new PasswordValidationService(userAccountRepository);
            IAccountSettingsService userAccountSettingsManager = new AccountSettingsService(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            bool result = await userAccountSettingsManager.ChangePasswordAsync(passwordModel.oldPassword, passwordModel.newPassword, passwordModel.id);
            Console.WriteLine("Password Changed");
            return result;
        }

        [HttpPost("changefontsize")]
        public async Task<bool> ChangeFontSizeAsync([FromBody] FontSizeModel fontSize)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IPasswordValidationService authenticationService = new PasswordValidationService(userAccountRepository);
            IAccountSettingsService userAccountSettingsManager = new AccountSettingsService(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            bool result = await userAccountSettingsManager.ChangeFontSizeAsync(fontSize.id, Int32.Parse(fontSize.fontSize));
            Console.WriteLine("Font Size Changed");
            return true;
        }


        [HttpPost("changeemail")]
        public async Task<bool> ChangeEmailAsync([FromBody] EmailModel email)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IPasswordValidationService authenticationService = new PasswordValidationService(userAccountRepository);
            IAccountSettingsService userAccountSettingsManager = new AccountSettingsService(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            bool result = await userAccountSettingsManager.ChangeEmailAsync(email.password, email.email, email.id);
            Console.WriteLine("Email Changed");
            return true;
        }

        [HttpPost("getFontSize")]
        public async Task<string> GetFontSizeAsync([FromBody] string id)
        {
            Console.WriteLine("Fethcing Font Size" + id);
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);


            return await userAccountSettingsRepository.GetFontSizeByID(Int32.Parse(id));
        }
        [HttpPost("changefontstyle")]
        public async Task<bool> ChangeFontStyleAsync([FromBody] FontStyleModel fontStyle)
        {
            Console.WriteLine("Font Style Changed");

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IPasswordValidationService authenticationService = new PasswordValidationService(userAccountRepository);
            IAccountSettingsService userAccountSettingsManager = new AccountSettingsService(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            bool result = await userAccountSettingsManager.ChangeFontStyleAsync(fontStyle.id, fontStyle.fontStyle);
            return true;
        }
        [HttpPost("getFontStyle")]
        public async Task<FontStyleModel> GetFontStyleAsync([FromBody] string id)
        {
            Console.WriteLine("Fethcing Font Style" + id);
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            FontStyleModel fontStyle = new FontStyleModel();
            fontStyle.fontStyle = await userAccountSettingsRepository.GetFontStyleByID(Int32.Parse(id));
            return fontStyle;
        }


        [HttpPost("changetheme")]
        public async Task<bool> ChangeThemeAsync([FromBody] ThemeModel theme)
        {
            Console.WriteLine("Font Style Changed");

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IPasswordValidationService authenticationService = new PasswordValidationService(userAccountRepository);
            IAccountSettingsService userAccountSettingsManager = new AccountSettingsService(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            bool result = await userAccountSettingsManager.ChangeThemeColorAsync(theme.id, theme.theme);
            return true;
        }
        [HttpPost("getTheme")]
        public async Task<ThemeModel> GetThemeAsync([FromBody] string id)
        {
            Console.WriteLine("Fethcing Font Style" + id);
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ThemeModel themeModel = new ThemeModel();
            themeModel.theme = await userAccountSettingsRepository.GetThemeColorByID(Int32.Parse(id));
            return themeModel;
        }
    }
}
