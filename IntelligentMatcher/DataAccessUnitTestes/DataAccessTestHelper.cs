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
            var query = "DBCC CHECKIDENT (@tableName, RESEED, @NEWSEEDNUMBER)";

            await dataGateway.SaveData(query, new
                                                 {
                                                    @tableName = tableName,
                                                    @NEWSEEDNUMBER = NEWSEEDNUMBER
                                                 },
                                         connectionString.SqlConnectionString);
        }
    }
}
