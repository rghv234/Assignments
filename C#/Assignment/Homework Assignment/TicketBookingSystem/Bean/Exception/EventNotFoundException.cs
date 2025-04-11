using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean.Exception
{
    public class EventNotFoundException : System.Exception { public EventNotFoundException(string message) : base(message) { } }
}
