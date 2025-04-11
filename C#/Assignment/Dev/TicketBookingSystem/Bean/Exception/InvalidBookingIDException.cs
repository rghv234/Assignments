using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean.Exception
{
    public class InvalidBookingIDException : System.Exception { public InvalidBookingIDException(string message) : base(message) { } }
}
