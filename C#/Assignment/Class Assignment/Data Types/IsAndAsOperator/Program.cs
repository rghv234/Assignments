using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsAndAsOperator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dynamic average = 156.89F;
            
            Boolean inttype = average is Int32;

            bool floattype = average is float;

            Boolean stringtype = average is String;

            Boolean doubletype = average is Double;    

            Boolean booleantype = average is Boolean;

            if (inttype == true || inttype == false)
            {
                Console.WriteLine(inttype);
            }

            if (floattype == true || floattype == false)
            {
                Console.WriteLine(floattype);
            }

            if (stringtype == true || stringtype == false)
            {
                Console.WriteLine(stringtype);
            }

            if (doubletype == true || doubletype == false)
            {
                Console.WriteLine(doubletype);
            }

            if (booleantype == true || booleantype == false)
            {
                Console.WriteLine(booleantype);
            }

            if (average is char)
            {
                Console.WriteLine(average is char);
            }


        }
    }
}
