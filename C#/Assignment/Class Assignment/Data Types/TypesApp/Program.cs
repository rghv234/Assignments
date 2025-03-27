using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = 100;
            string name = "test";
            Console.WriteLine(number);
            Console.WriteLine(name);
            string cname = name;
            Console.WriteLine(cname);
            cname = "python";
            Console.WriteLine(cname);
            Int64 mark_english = 85;
            Int64 mark_math = 90;
            Int64 mark_science = 95;
            Int64 mobilenum = 9689748589;

            Int64 Total_Marks = mark_math + mark_science + mark_english;

            Console.WriteLine("Output Using Writeline");
            Console.WriteLine(mark_math);
            Console.WriteLine(mark_science);
            //Console.WriteLine(mobilenum);
            Console.WriteLine(mark_english);

            //output with write method
            Console.WriteLine("Output Using write");
            Console.Write(mark_english);
            Console.Write(mark_math);
            Console.Write(mark_science);

            //formatting output with functions
            Console.WriteLine("Math \t Science \t English \t Total \n {0} \t {1} \t\t {2} \t\t {3}", mark_math, mark_science, mark_english, Total_Marks);
            string.Format("Math \t Science \t English \t Total \n {0} \t {1} \t\t {2} \t\t {3}", mark_math, mark_science, mark_english, Total_Marks);

            Console.ReadKey();
        }
    }
}
