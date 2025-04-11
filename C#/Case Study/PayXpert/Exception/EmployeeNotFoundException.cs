using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Exception
{
    public class EmployeeNotFoundException : System.Exception
    {
        public EmployeeNotFoundException() : base("Employee not found.") { }
        public EmployeeNotFoundException(string message) : base(message) { }
        public EmployeeNotFoundException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
