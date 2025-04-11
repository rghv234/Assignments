using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Exception
{
    public class InvalidInputException : System.Exception
    {
        public InvalidInputException() : base("Invalid input data provided.") { }
        public InvalidInputException(string message) : base(message) { }
        public InvalidInputException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
