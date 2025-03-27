using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryAndTryParse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input value for int32");

            //Use parse method

            Int32 number = Int32.Parse(Console.ReadLine());

            //Using try parse

            string input = Console.ReadLine();

            Int64 result;
            bool number2 = Int64.TryParse(input, out result);

            if (number2 == true)
            {
                Console.WriteLine("Result after parsing " + result);
            }
            else
            {
                Console.WriteLine("no input provided");
            }

            Console.ReadKey();
        }
    }
}
