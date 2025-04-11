using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Exception
{
    public class DatabaseConnectionException : System.Exception
    {
        public DatabaseConnectionException() : base("Database connection error.") { }
        public DatabaseConnectionException(string message) : base(message) { }
        public DatabaseConnectionException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
