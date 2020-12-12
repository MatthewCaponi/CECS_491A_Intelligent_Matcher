using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {

        }
        public UserNotFoundException(string message)
            : base(message)
        {

        }

    }
}
