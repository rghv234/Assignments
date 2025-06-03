using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> langs = new List<string>();
            langs.Add("Pearl");
            langs.Add("R ");
            langs.Add("C#");
            langs.Add("VB");
            langs.Add("Java");
            langs.Insert(2, "C++");
            foreach (var item in langs)
            {
                Console.WriteLine(item);
            }
            langs.Remove("R ");
            Console.WriteLine(" After removing R " + langs.Count);
            Console.WriteLine(" Java is avaialable or not " + langs.Contains("Java"));

            List<int> integerlist = new List<int>() { 34, 567, 87, 34, 67, 89, 87, 66, 83, 67, 35 };
            int arraynum = 0;
            Console.WriteLine(" List of integers are");
            foreach (var item in integerlist)
            {
                if (item % 2 == 0)
                    arraynum++;
                Console.WriteLine(item);
            }
                

        }
    
    }
}
