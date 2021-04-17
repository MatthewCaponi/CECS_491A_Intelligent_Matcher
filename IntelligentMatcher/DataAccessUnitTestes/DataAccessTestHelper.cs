using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessUnitTestes
{
    public class DataAccessTestHelper
    {
        public static async Task ReseedAsync(string tableName, int NEWSEEDNUMBER, IConnectionStringData connectionString, 
            IDataGateway dataGateway)
        {
            var storedProcedure = "dbo.Testing_Reseed";

            await dataGateway.Execute(storedProcedure, new
                                                 {
                                                    @tableName = tableName,
                                                    @NEWSEEDNUMBER = NEWSEEDNUMBER
                                                 },
                                         connectionString.SqlConnectionString);
        }
    }
}
