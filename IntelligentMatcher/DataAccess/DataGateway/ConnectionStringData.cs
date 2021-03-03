using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ConnectionStringData : IConnectionStringData
    {
        public string SqlConnectionString { get; set; } = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IntelligentMatcherUserDatabase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
