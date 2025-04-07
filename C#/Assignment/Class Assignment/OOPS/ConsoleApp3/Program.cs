using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{

    public class Calculator
    {
        public int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        public double Add(double double1, double double2)
        {
            return double1 + double2;
        }

        public string Add(string str1, string str2)
        {
            return str1 + str2;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            int sum1 = calculator.Add(1, 2);
            Console.WriteLine(sum1);

            double sum2 = calculator.Add(2.5, 3.6);
            Console.WriteLine(sum2);

            string sum3 = calculator.Add("hello", "world");
            Console.WriteLine(sum3);
        }
    }
}
