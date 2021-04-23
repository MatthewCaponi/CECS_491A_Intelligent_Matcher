using Dapper;
using Exceptions;
using Models.User_Access_Control;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public ResourceRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ResourceModel>> GetAllResources()
        {
            string storedProcedure = "dbo.Resource_Get_All";

            return await _dataGateway.LoadData<ResourceModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<ResourceModel> GetResourceById(int id)
        {
            string storedProcedure = "dbo.Resource_Get_ById";

            var row = await _dataGateway.LoadData<ResourceModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateResource(ResourceModel model)
        {
            try
            {
                var storedProcedure = "dbo.Resource_Create";

                DynamicParameters p = new DynamicParameters();

                p.Add("name", model.Name);
                p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

                return p.Get<int>("Id");
            }
            catch(SqlCustomException e)
            {
                throw new SqlCustomException("Name is Missing to create Resource.", e.InnerException);
            }
        }

        public async Task<int> UpdateResource(ResourceModel model)
        {
            var storedProcedure = "dbo.Resource_Update";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = model.Id,
                                             name = model.Name
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteResource(int id)
        {
            var storedProcedure = "dbo.Resource_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
