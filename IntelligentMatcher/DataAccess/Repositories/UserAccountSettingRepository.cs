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
            var query = "delete from [UserAccountSettings] where UserId = @UserId";

            return await _dataGateway.SaveData(query,
                                         new
                                         {
                                             UserId = userId
                                         },
                                         _connectionString.SqlConnectionString);
        }
        public async Task<IEnumerable<UserAccountSettingsModel>> GetAllSettings()
        {
            var query = "select * from [UserAccountSettings]";

            return await _dataGateway.LoadData<UserAccountSettingsModel, dynamic>(query,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }
        public async Task<bool> CreateUserAccountSettings(UserAccountSettingsModel model)
        {
            
                var query = "insert into [UserAccountSettings]([UserId], [FontSize], [FontStyle], [ThemeColor])" +
                            "values (@UserId, @FontSize, @FontStyle, @ThemeColor); " +
                            "set @Id = SCOPE_IDENTITY(); ";

                DynamicParameters p = new DynamicParameters();

                p.Add("UserId", model.UserId);
                p.Add("FontSize", model.FontSize);
                p.Add("FontStyle", model.FontStyle);
                p.Add("ThemeColor", model.ThemeColor);
                p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                await _dataGateway.SaveData(query, p, _connectionString.SqlConnectionString);
                return true;            

        }

        public async Task<UserAccountSettingsModel> GetUserAccountSettingsByUserId(int userId)
        {

            var query = "select [Id], [UserId], [FontSize], [FontStyle], [ThemeColor] " +
                        "from [UserAccountSettings] where UserId = @UserId;";

            var row = await _dataGateway.LoadData<UserAccountSettingsModel, dynamic>(query,
                new
                {
                    UserId = userId
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> UpdateFontSize(int id, int fontSize)
        {
            var query = "update [UserAccountSettings] set FontSize = @FontSize where UserId = @Id;";

            return await _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id,
                                             FontSize = fontSize
                                         },
                                         _connectionString.SqlConnectionString);
        }
        public async Task<int> UpdateFontStyle(int id, string fontStyle)
        {
            var query = "update [UserAccountSettings] set FontStyle = @FontStyle where UserId = @Id;";

            return await _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id,
                                             FontStyle = fontStyle
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateThemeColor(int id, string themeColor)
        {
            var query = "update [UserAccountSettings] set ThemeColor = @Theme where UserId = @Id;";

            return await _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id,
                                             ThemeColor = themeColor
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<string> GetThemeColorByID(int id)
        {
            var query = "select [ThemeColor]" +
                       "from [UserAccountSettings] where UserId = @Id";

            var row = await _dataGateway.LoadData<string, dynamic>(query,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<string> GetFontStyleByID(int id)
        {
            var query = "select [FontStyle]" +
                       "from [UserAccountSettings] where UserId = @Id";

            var row = await _dataGateway.LoadData<string, dynamic>(query,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<string> GetFontSizeByID(int id)
        {
            var query = "select [FontSize]" +
                       "from [UserAccountSettings] where UserId = @Id";

            var row = await _dataGateway.LoadData<string, dynamic>(query,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

    }
}
