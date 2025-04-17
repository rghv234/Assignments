using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HospitalManagement.util
{
    public static class PropertyUtil
    {
        public static string GetPropertyString()
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Connection string 'DefaultConnection' not found in appsettings.json.");
                }
                return connectionString;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading connection string from appsettings.json: " + ex.Message);
            }
        }
    }
}
