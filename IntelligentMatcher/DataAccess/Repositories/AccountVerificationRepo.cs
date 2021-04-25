using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AccountVerificationRepo : IAccountVerificationRepo
    {
        private const int TOKEN_LENGTH = 200;

        private string GenerateToken()
        {


            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < TOKEN_LENGTH; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            string token = str_build.ToString();

            return token;

        }

        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;
        public AccountVerificationRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAccountVerification(int userId)
        {
            var storedProcedure = "dbo.AccountVerification_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("Token", GenerateToken());
            p.Add("UserId", userId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);


            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<string> GetStatusTokenByUserId(int userId)
        {
            var storedProcedure = "dbo.AccountVerification_GetStatusToken_ByUserId";

            var row = await _dataGateway.LoadData<string, dynamic>(storedProcedure,
                new
                {
                    UserId = userId
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> UpdateAccountStatusToken(int userId)
        {
            var storedProcedure = "dbo.AccountVerification_Update_AccountStatusToken";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Token = GenerateToken(),
                                             UserId = userId
                                         },
                                         _connectionString.SqlConnectionString);
        }


        public async Task<int> DeleteAccountVerificationById(int id)
        {
 
                var storedProcedure = "dbo.AccountVerification_Delete_ById";

                return await _dataGateway.Execute(storedProcedure,
                                             new
                                             {
                                                 Id = id
                                             },
                                             _connectionString.SqlConnectionString);
            
        }

        public async Task<IEnumerable<VerficationTokenModel>> GetAllAccountVerifications()
        {
            string storedProcedure = "dbo.AccountVerification_Get_All";


            return await _dataGateway.LoadData<VerficationTokenModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

    }
}
