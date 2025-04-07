using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    internal class Program
    {
        static class MathUtils
        {
            public static int Add(int a, int b)
            {
                return a + b;
            }
            public static double CalculateCircleArea(double radius)
            {
                return Math.PI * radius * radius;
            }
        }
        static void Main(string[] args)
        {
            int sum = MathUtils.Add(2, 3); // Calling the static Add() method
            double area = MathUtils.CalculateCircleArea(5.0); // Calling the static CalculateCircleArea() method
        }
    }
}
