using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Security;
using DataAccess;
using DataAccess.Repositories;

using UserAccountSettings;
using System.Collections.Generic;
using WebApi.Controllers;
using AuthorizationPolicySystem;
using AuthorizationResolutionSystem;

using Models;
namespace WebApi.Controllers
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
    public class UserAccountSettingsController : ApiBaseController
    {

        private readonly IAccountSettingsService _userAccountSettingsService;
        private readonly IUserAccountSettingsRepository _userAccountSettingsRepository;
        private readonly IAuthorizationPolicyManager _authorizationPolicyManager;
        private readonly IAuthorizationResolutionManager _authorizationResolutionManager;

        public UserAccountSettingsController(IAccountSettingsService accountSettingsService, IUserAccountSettingsRepository userAccountSettingsRepository,
             IAuthorizationPolicyManager authorizationPolicyManager, IAuthorizationResolutionManager authorizationResolutionManager)
        {
            _userAccountSettingsService = accountSettingsService;
            _userAccountSettingsRepository = userAccountSettingsRepository;
            _authorizationPolicyManager = authorizationPolicyManager;
            _authorizationResolutionManager = authorizationResolutionManager;
        }

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


        [HttpPost]
        public async Task<ActionResult<bool>> DeleteAccountAsync([FromBody] DeleteModel deleteModel)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", deleteModel.id.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "account:delete"
                    }, claims);
                return await _userAccountSettingsService.DeleteAccountByUserIDAsync(deleteModel.id, deleteModel.password);

            }
            catch
            {
                return false;
            }

        }
        [HttpPost]
        public async Task<ActionResult<bool>> PasswordChangeAsync([FromBody] PasswordModel passwordModel)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", passwordModel.id.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "account:passwordchange"
                    }, claims);
                return await _userAccountSettingsService.ChangePasswordAsync(passwordModel.oldPassword, passwordModel.newPassword, passwordModel.id);

            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> ChangeFontSizeAsync([FromBody] FontSizeModel fontSize)
        {

            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", fontSize.id.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "account.fontszie:change"
                    }, claims);
                return await _userAccountSettingsService.ChangeFontSizeAsync(fontSize.id, Int32.Parse(fontSize.fontSize));

            }
            catch
            {
                return false;
            }
        }


        [HttpPost]
        public async Task<ActionResult<bool>> ChangeEmailAsync([FromBody] EmailModel email)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", email.id.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "account.email:change"
                    }, claims);
                return await _userAccountSettingsService.ChangeEmailAsync(email.password, email.email, email.id);

            }
            catch
            {
                return false;
            }


        }

        [HttpPost]
        public async Task<ActionResult<string>> GetFontSizeAsync([FromBody] string id)
        {

            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", id.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "account.fontSize:get"
                    }, claims);
                return await _userAccountSettingsRepository.GetFontSizeByID(Int32.Parse(id));

            }
            catch
            {
                return StatusCode(404);
            }
        }
        [HttpPost]
        public async Task<ActionResult<bool>> ChangeFontStyleAsync([FromBody] FontStyleModel fontStyle)
        {

            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", fontStyle.id.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "account.fontstyle:change"
                    }, claims);
                return await _userAccountSettingsService.ChangeFontStyleAsync(fontStyle.id, fontStyle.fontStyle);

            }
            catch
            {
                return false;
            }
        }
        [HttpPost]
        public async Task<ActionResult<FontStyleModel>> GetFontStyleAsync([FromBody] string id)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", id.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "account.fontstyle:get"
                    }, claims);
                FontStyleModel fontStyle = new FontStyleModel();
                fontStyle.fontStyle = await _userAccountSettingsRepository.GetFontStyleByID(Int32.Parse(id));
                return fontStyle;
            }
            catch
            {
                return StatusCode(404);

            }
        }


        [HttpPost]
        public async Task<ActionResult<bool>> ChangeTheme([FromBody] ThemeModel theme)
        {

            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", theme.id.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "account.theme:change"
                    }, claims);
                return await _userAccountSettingsService.ChangeThemeColorAsync(theme.id, theme.theme);

            }
            catch
            {
                return false;
            }
        }
        [HttpPost]
        public async Task<ActionResult<ThemeModel>> GetTheme([FromBody] string id)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", id.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "account.theme:get"
                    }, claims);
                ThemeModel themeModel = new ThemeModel();
                themeModel.theme = await _userAccountSettingsRepository.GetThemeColorByID(Int32.Parse(id));
                return themeModel;
            }


            catch
            {
                return StatusCode(404);

            }
        }
    }
}
