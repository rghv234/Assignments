using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial;
using Calculation;

namespace NamespaceDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Financial.Math mathObject = new Financial.Math();
            Calculation.Math mathFunc = new Calculation.Math();

            Console.WriteLine(mathFunc);
            Console.WriteLine(mathObject);

            Financial.Math mathObject1 = new Financial.Math();
            Calculation.Math mathFunc1 = new Calculation.Math();

        }
    }
}
