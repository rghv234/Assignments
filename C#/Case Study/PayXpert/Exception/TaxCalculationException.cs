using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Exception
{
    public class TaxCalculationException : System.Exception
    {
        public TaxCalculationException() : base("Error calculating tax.") { }
        public TaxCalculationException(string message) : base(message) { }
        public TaxCalculationException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
