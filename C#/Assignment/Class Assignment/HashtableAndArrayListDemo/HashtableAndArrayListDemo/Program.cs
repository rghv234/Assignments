using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections; // Required for Hashtable

namespace HashtableAndArrayListDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hashtable students = new Hashtable();
            students.Add("101", "Alice");
            students.Add("102", "Bob");
            students.Add("103", "Charlie");
            students.Add("104", "Diana");
            students.Add("105", "Eve");

            Console.WriteLine("Students in the Hashtable:");
            foreach (DictionaryEntry entry in students)
            {
                Console.WriteLine($"ID: {entry.Key}, Name: {entry.Value}");
            }

            ArrayList studentList = new ArrayList() { "Laptop", "Tablet", "Smartphone", "Smartwatch" };
            Console.WriteLine("\nItems in the ArrayList:");
            foreach (var item in studentList)
            {
                Console.WriteLine(item);
            }

            int searchKey = 103;
            if (students.ContainsKey(searchKey.ToString()))
            {
                students[searchKey.ToString()] = "Charlotte"; // Update the value
                Console.WriteLine($"\nStudent with ID {searchKey} found: {students[searchKey.ToString()]}");
            }
            else
            {
                Console.WriteLine($"\nStudent with ID {searchKey} not found.");
            }

            if (studentList.Count > 2)
            {
                studentList.RemoveAt(2); // Remove the third item (Smartphone)
                Console.WriteLine("\nAfter removing the third item from the ArrayList:");
                foreach (var item in studentList)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("\nArrayList does not have enough items to remove the third one.");
            }

            studentList.Remove("Tablet"); // Remove "Tablet" from the ArrayList
            Console.WriteLine("\nAfter removing 'Tablet' from the ArrayList:");
            foreach (var item in studentList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"Total entries in Hashtable: {students.Count}");
            students.Clear(); // Clear the Hashtable
            Console.WriteLine("Hashtable cleared. Total entries now: " + students.Count);

            ArrayList nums = new ArrayList() { 5, 2, 4, 6, 8 };
            nums.Sort(); // Sort the ArrayList
            Console.WriteLine("\nSorted ArrayList of numbers:");
            foreach (var num in nums)
            {
                Console.WriteLine(num);
            }
            nums.Reverse(); // Reverse the ArrayList
            Console.WriteLine("\nReversed ArrayList of numbers:");
            foreach (var num in nums)
            {
                Console.WriteLine(num);
            }

            Hashtable hashtable = new Hashtable();
            hashtable.Add("A", "Apple");
            try
            {
                hashtable.Add("A", "Avocado"); // Attempt to add a duplicate key
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("\nError: " + ex.Message); // Catch the exception for duplicate key
            }

            studentList.Insert(1, "Mouse"); // Insert "Mouse" at index 1
            Console.WriteLine("\nAfter inserting 'Mouse' at index 1 in the ArrayList:");
            foreach (var student in studentList)
            {
                Console.WriteLine(student);
            }
            hashtable.Add("B", "Banana");
            hashtable.Add("C", "Cherry");

            Console.WriteLine("\nHashtable after adding more entries:");
            foreach (DictionaryEntry entry in hashtable)
            {
                Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            }

            Console.WriteLine("\nPress any key to exit...");
        }
    }
}
