using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean.Util
{
    public class DBUtil
    {
        private static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ticketbookingsystem;Trusted_Connection=True;";

        public static SqlConnection GetDBConn()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
