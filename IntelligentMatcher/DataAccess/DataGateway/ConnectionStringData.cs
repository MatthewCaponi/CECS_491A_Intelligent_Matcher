using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ConnectionStringData : IConnectionStringData
    {
        public string SqlConnectionString { get; set; } = "Server=tcp:infinimuse-sql-server.database.windows.net,1433;Initial Catalog=InfiniMuseSqlServer;Persist Security Info=False;User ID=Sql491;Password=Hello491sql32@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
