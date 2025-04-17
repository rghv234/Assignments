using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.util
{
    public static class DBConnection
    {
        private static SqlConnection connection;

        public static SqlConnection GetConnection()
        {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed)
            {
                string connectionString = PropertyUtil.GetPropertyString();
                connection = new SqlConnection(connectionString);
            }
            return connection;
        }
    }
}
