using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Models;
using System.Threading.Tasks;
using System.Linq;
using Dapper;

namespace DataAccess.Repositories
{
    public class UserAccountSettingRepository : IUserAccountSettingsRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserAccountSettingRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }
        public async Task<int> DeleteUserAccountSettingsByUserId(int userId)
        {
            var storedProcedure = "dbo.UserAccountSettings_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId
                                         },
                                         _connectionString.SqlConnectionString);
        }
        public async Task<IEnumerable<UserAccountSettingsModel>> GetAllSettings()
        {
            var storedProcedure = "dbo.UserAccountSettings_Get_All";

            return await _dataGateway.LoadData<UserAccountSettingsModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }
        public async Task<bool> CreateUserAccountSettings(UserAccountSettingsModel model)
        {

            var storedProcedure = "dbo.UserAccountSettings_Create";

                DynamicParameters p = new DynamicParameters();

                p.Add("UserId", model.UserId);
                p.Add("FontSize", model.FontSize);
                p.Add("FontStyle", model.FontStyle);
                p.Add("ThemeColor", model.ThemeColor);
                p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);
                return true;            

        }

        public async Task<UserAccountSettingsModel> GetUserAccountSettingsByUserId(int userId)
        {

            var storedProcedure = "dbo.UserAccountSettings_Get_ById";

            var row = await _dataGateway.LoadData<UserAccountSettingsModel, dynamic>(storedProcedure,
                new
                {
                    UserId = userId
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> UpdateFontSize(int id, int fontSize)
        {
            var storedProcedure = "dbo.UserAccountSettings_Update_FontSize";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = id,
                                             FontSize = fontSize
                                         },
                                         _connectionString.SqlConnectionString);
        }
        public async Task<int> UpdateFontStyle(int userId, string fontStyle)
        {
            var storedProcedure = "dbo.UserAccountSettings_Update_FontStyle";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             FontStyle = fontStyle
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateThemeColor(int userId, string themeColor)
        {
            var storedProcedure = "dbo.UserAccountSettings_Update_ThemeColor";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             ThemeColor = themeColor,
                                             UserId = userId
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<string> GetThemeColorByID(int userId)
        {
            var storedProcedure = "dbo.UserAccountSettings_GetThemeColor_ById";

            var row = await _dataGateway.LoadData<string, dynamic>(storedProcedure,
                new
                {
                    UserId = userId
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<string> GetFontStyleByID(int id)
        {
            var storedProcedure = "dbo.UserAccountSettings_GetFontStyle_ById";

            var row = await _dataGateway.LoadData<string, dynamic>(storedProcedure,
                new
                {
                    UserId = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<string> GetFontSizeByID(int id)
        {
            var storedProcedure = "dbo.UserAccountSettings_GetFontSize_ById";

            var row = await _dataGateway.LoadData<string, dynamic>(storedProcedure,
                new
                {
                    UserId = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

    }
}
