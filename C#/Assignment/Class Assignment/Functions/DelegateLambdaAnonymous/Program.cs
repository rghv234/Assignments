using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateLambdaAnonymous
{
    internal class Program
    {
        public delegate int Operation(int x, int y);
        public static int Add(int a, int b) => a + b;   

        Operation op1 = delegate(int a, int b) { return a + b; };   

        Operation op2 = (a, b) => { return a + b; };
        static void Main(string[] args)
        {
            Operation op3 = Add;
            Console.WriteLine(op3(1, 2));
            
        }
    }
}
