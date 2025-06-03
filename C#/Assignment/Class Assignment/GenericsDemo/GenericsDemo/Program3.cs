using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsDemo
{
    // Define a generic delegate
    public delegate T Operation<T>(T a, T b);

    class Program3
    {
        // Add method for int
        static int AddInt(int x, int y)
        {
            return x + y;
        }

        // Add method for double
        static double AddDouble(double x, double y)
        {
            return x + y;
        }

        static void Main(string[] args)
        {
            // Use delegate with int
            Operation<int> addInt = AddInt;
            Console.WriteLine("Sum of ints: " + addInt(10, 20));

            // Use delegate with double
            Operation<double> addDouble = AddDouble;
            Console.WriteLine("Sum of doubles: " + addDouble(5.5, 6.3));

            Console.ReadKey();
        }
    }
}
