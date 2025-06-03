using System;
using System.Collections;

namespace HashTableDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hashtable hs = new Hashtable();
            hs.Add("F1", "Apple");
            hs.Add("F2", "Orange");
            hs.Add("F4", "Mosambi");
            hs.Add("F3", "Pomegranate");
            hs["F7"] = "Jackfruit";
            hs["F5"] = "Pineapple";
            hs["F6"] = "Amla";

            Console.WriteLine("The First Fruit: {0}", hs["F1"]);
            Console.WriteLine("The Last Fruit: {0}", hs["F6"]);
            Console.WriteLine("Total no. of items in hs: " + hs.Count);

            hs.Add(10, "Grapes");
            Console.WriteLine("Total no. of items in hs after adding key 10: " + hs.Count);
            Console.WriteLine("The Fruit at key 10: {0}", hs[10]);

            Console.WriteLine("Is key 'F6' present? " + hs.Contains("F6")); // Checks if a value exists (not key!)
            Console.WriteLine("Is key 'F5' present? " + hs.ContainsKey("F5"));

            Console.WriteLine("\nAfter removing two items from hashtable");
            hs.Remove("F5");
            hs.Remove("F3");
            Console.WriteLine("Total no. of items in hs: " + hs.Count);

            hs.Add("F5", "xxx");
            Console.WriteLine("The First Fruit: {0}", hs["F1"]);
            Console.WriteLine("The Last Fruit: {0}", hs["F6"]);
            Console.WriteLine("Total no. of items in hs: " + hs.Count);

            // Copying values to array
            Console.WriteLine("\nCopying values from Hashtable to array A3");
            string[] A3 = new string[10];
            hs.Values.CopyTo(A3, 0);
            Console.WriteLine("Array Elements in A3:");
            foreach (var item in A3)
            {
                Console.WriteLine(item);
            }

            // Copying keys to array
            Console.WriteLine("\nCopying keys from Hashtable to array h2");
            object[] h2 = new object[10];
            hs.Keys.CopyTo(h2, 1);
            Console.WriteLine("Array Elements in h2:");
            foreach (var item in h2)
            {
                Console.WriteLine(item);
            }

            // Displaying Keys and Values from Hashtable
            Console.WriteLine("\nAll keys and values in Hashtable:");
            foreach (DictionaryEntry entry in hs)
            {
                Console.WriteLine("Key: {0}, Value: {1}", entry.Key, entry.Value);
            }

            // Clear all items from hashtable
            Console.WriteLine("\nTotal no. of items in hs before clear: " + hs.Count);
            hs.Clear();
            Console.WriteLine("Total no. of items in hs after clear: " + hs.Count);

            hs.Clear();
            Console.WriteLine(" Total no.of items in hs after clear " + hs.Count);

            Console.ReadKey();
        }
    }
}
