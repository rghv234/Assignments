using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine(i);
            }
        }
    }

    internal class AsOpDemo
    {
        static void Main()
        {
            object[] strobj = { "str1", "str2", "str3" };
            string str = strobj[1] as string;
            Console.WriteLine(str);
        }

    }
}
