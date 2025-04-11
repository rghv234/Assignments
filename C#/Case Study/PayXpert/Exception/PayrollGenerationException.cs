using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Exception
{
    public class PayrollGenerationException : System.Exception
    {
        public PayrollGenerationException() : base("Error generating payroll.") { }
        public PayrollGenerationException(string message) : base(message) { }
        public PayrollGenerationException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
