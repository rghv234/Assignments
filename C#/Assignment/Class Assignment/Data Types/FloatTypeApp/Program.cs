using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloatTypeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float number = 123.15F;
            double number2 = 5689.26D;
            decimal number3 = 89325.15m;

            //parsing using convert
            string numberFloat = Console.ReadLine();
            number = Convert.ToSingle(numberFloat);

            //parsing using parse method
            string numberDouble = Console.ReadLine();
            number2 = float.Parse(numberDouble);

            //parsing using parse method
            string numberDecimal = Console.ReadLine();
            number2 = float.Parse(numberDecimal);

            //parsing string

            Console.WriteLine(number);
            Console.WriteLine(number2); 
            Console.WriteLine(number3);

            Console.ReadKey();
        }
    }
}
