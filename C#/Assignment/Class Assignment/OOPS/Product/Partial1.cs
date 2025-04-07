using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Product.Partial1;

namespace Product
{
    internal class Partial1
    {
        public partial class Computer
        {
            //Super
            public Computer()
            {
                Console.WriteLine("This is default constrcutor of computer class");
}
        public string Start()
        {
            return "Startting a Computer";
        }
        public string Stop()
        {
            return "Stopping a Computer";
        }
    }
}
}
