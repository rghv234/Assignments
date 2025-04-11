using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayXpert.Exception;

namespace PayXpert.DAO
{
    public class DatabaseContext : IDisposable
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public DatabaseContext()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=payxpert;Trusted_Connection=True;";
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection Connection
        {
            get
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (SqlException ex)
                    {
                        throw new DatabaseConnectionException("Failed to establish database connection.", ex);
                    }
                }
                return connection;
            }
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error executing database command.", ex);
                }
            }
        }

        public SqlDataReader ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            SqlCommand command = new SqlCommand(query, Connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            try
            {
                return command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error executing database query.", ex);
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
        }
    }
}
