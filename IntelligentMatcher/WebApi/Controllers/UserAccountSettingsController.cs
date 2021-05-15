using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Security;
using DataAccess;
using DataAccess.Repositories;

using UserAccountSettings;

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
    public class UserAccountSettingsController : ControllerBase
    {

        private readonly IAccountSettingsService _userAccountSettingsService;
        private readonly IUserAccountSettingsRepository _userAccountSettingsRepository;

        public UserAccountSettingsController(IAccountSettingsService accountSettingsService, IUserAccountSettingsRepository userAccountSettingsRepository)
        {
            _userAccountSettingsService = accountSettingsService;
            _userAccountSettingsRepository = userAccountSettingsRepository;
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
