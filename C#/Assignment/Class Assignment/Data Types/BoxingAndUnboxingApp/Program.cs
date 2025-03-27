using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingAndUnboxingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //converting value to reference is boxing

            Int32 boxvalue = 789;

            object boxvalue2 = boxvalue; 
            
            Console.WriteLine(boxvalue2);

            //converting reference to value is unboxing

            Int32 unboxvalue = (Int32)boxvalue2;

            Console.WriteLine("After explicit conversion "+unboxvalue);

            unboxvalue = Convert.ToInt32(boxvalue2);

            Console.WriteLine("using convert class " + unboxvalue);

            Console.ReadKey();  
        }
    }
}
