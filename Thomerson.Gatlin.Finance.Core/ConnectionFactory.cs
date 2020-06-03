using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Thomerson.Gatlin.Finance.Core
{
    public class ConnectionFactory
    {
        private static readonly string ConnectionString = "Server=.;Database=Finance;User ID=sa;Password=asdf@1234;Trusted_Connection=False;";

        public static IDbConnection CreateConnection<T>() where T : IDbConnection, new()
        {
            IDbConnection connection = new T();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
        }

        public static IDbConnection CreateSqlConnection()
        {
            return CreateConnection<SqlConnection>();
        }
    }
}
