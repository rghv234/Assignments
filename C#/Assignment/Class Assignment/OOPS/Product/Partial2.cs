using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    internal class Partial2
    {
        partial class Computer
        {
            //Micro Computers
            public Computer(string name)
            {
                Console.WriteLine($"Computer Type:{ name}");
            }
            public string Reboot()
            {
                return "Rebooting a Computer";
            }
        }
    }
}
