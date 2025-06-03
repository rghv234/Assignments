using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace GenericsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool result = Calculator.AreEqual<string, int>("10", 10);
            if (result)
            {
                Console.WriteLine("The strings are equal.");
            }
            else
            {
                Console.WriteLine("The strings are not equal.");
            }
            Calculator.Greet<string, string>("Alice", "Bob");
        }
    }

    class Calculator 
    {
        public static bool AreEqual<T1, T2>(T1 a, T2 b) 
        {
            return a.Equals(b);
        }
        public static void Greet<Q1, Q2>(Q1 name, Q2 name2)
        {
            Console.WriteLine($"Hello {name} and {name2}!");
        }
    }
}
