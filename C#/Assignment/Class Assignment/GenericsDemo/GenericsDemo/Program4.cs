using System;
using System.Collections;

namespace GenericsDemo
{
    internal class Program4
    {
        static void Main(string[] args)
        {
            ArrayList myarrayList = new ArrayList();
            myarrayList.Add(100);
            myarrayList.Add("Geetha");
            myarrayList.Add('S');
            myarrayList.Add(3.14);

            Console.WriteLine("List of items in the myArrayList:");
            foreach (var item in myarrayList)
                Console.WriteLine(item);

            ArrayList myarrayList2 = new ArrayList() { "Apple", "300", "Bread" };
            myarrayList.AddRange(myarrayList2);

            Console.WriteLine("\nAfter AddRange (2):");
            foreach (var item in myarrayList)
                Console.WriteLine(item);

            myarrayList.Add(3.14);
            Console.WriteLine("\nList after adding 3.14 again:");
            foreach (var item in myarrayList)
                Console.WriteLine(item);

            myarrayList.Insert(1, "New inserted Item");
            Console.WriteLine("\nAfter Insert(1):");
            foreach (var item in myarrayList)
                Console.WriteLine(item);

            ArrayList veggies = new ArrayList() { "Carrot", "Beans", "Potato", "Peas" };
            myarrayList.InsertRange(0, veggies);

            Console.WriteLine("\nAfter InsertRange(0, veggies):");
            foreach (var item in myarrayList)
                Console.WriteLine(item);

            Console.WriteLine($"\nCount before RemoveRange: {myarrayList.Count}");

            myarrayList.RemoveRange(1, 3);
            Console.WriteLine("\nAfter RemoveRange(1, 3):");
            foreach (var item in myarrayList)
                Console.WriteLine(item);

            Console.WriteLine($"\nFinal Count: {myarrayList.Count}");
        }
    }
}
