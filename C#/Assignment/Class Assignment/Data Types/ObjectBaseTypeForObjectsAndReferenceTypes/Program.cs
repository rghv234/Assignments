using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBaseTypeForObjectsAndReferenceTypes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int64 number = 1258;

            string name = "abc";

            Console.WriteLine("for int64");

            Console.WriteLine("Convert to string \t {0}", number.ToString());

            Console.WriteLine("GetHash code to get hash code of number", number.GetHashCode()); 
            Console.WriteLine("Get type of variable" + number.GetType());
            Console.WriteLine("compare the value \t" 
                + number.Equals(number));

            Console.WriteLine("for string");

            Console.WriteLine("Convert to string \t {0}", name.ToString());

            Console.WriteLine("GetHash code to get hash code of number", name.GetHashCode());
            Console.WriteLine("Get type of variable" + name.GetType());
            Console.WriteLine("compare the value \t"
                + number.Equals(number));

            Console.ReadKey();  
        }
    }
}
