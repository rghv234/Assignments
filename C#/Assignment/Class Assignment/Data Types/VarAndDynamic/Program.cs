using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarAndDynamic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var variablevalue = 156.15F;

            Console.WriteLine("Type of variable " + variablevalue.GetType());

            dynamic dynamicvalue = 789;

            Console.WriteLine("Type of dynamic " + dynamicvalue.GetType());

            Console.ReadKey();
        }
    }
}
