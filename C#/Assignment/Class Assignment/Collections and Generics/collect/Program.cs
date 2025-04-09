using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace collect
{
    internal class Program
    {
        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        class Task
        {
            public string Name { get; set; }
            public DateTime Deadline { get; set; }
            public Task(string name, DateTime deadline)
            {
                Name = name;
                Deadline = deadline;
            }
            public override string ToString()
            {
                return $"{Name}: {Deadline.ToShortDateString()}";
            }
        }

        class Order
        {
            public int OrderId { get; set; }
            public string CustomerName { get; set; }
            public decimal TotalAmount { get; set; }
            public Order(int orderId, string customerName, decimal totalAmount)
            {
                OrderId = orderId;
                CustomerName = customerName;
                TotalAmount = totalAmount;
            }
        }

        class Book
        {
            public int BookId { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public Book(int bookId, string title, string author)
            {
                BookId = bookId;
                Title = title;
                Author = author;
            }
            public override string ToString()
            {
                return $"Book ID: {BookId}, Title: {Title}, Author: { Author}";
            }
        }
        static void Main(string[] args)
        {
            // Create a Queue to simulate a print job queue
            Queue printJobQueue = new Queue();
            // Enqueue print jobs
            printJobQueue.Enqueue("Document1.pdf");
            printJobQueue.Enqueue("Document2.docx");
            printJobQueue.Enqueue("Document3.txt");
            while (printJobQueue.Count > 0)
            {
                string currentJob = (string)printJobQueue.Dequeue();
                Console.WriteLine("Printing: " + currentJob);
                // Simulate printing delay
                System.Threading.Thread.Sleep(2000);
            }
            Console.WriteLine("All print jobs completed.");

            Stack browserHistory = new Stack();
            // Visit websites and add them to the history

            browserHistory.Push("https://www.example.com");
            browserHistory.Push("https://www.google.com");
            browserHistory.Push("https://www.openai.com");
            // Simulate navigating back by popping the most recent website from the history

            string currentWebsite = (string)browserHistory.Pop();
            Console.WriteLine("Current Website: " + currentWebsite);
            // Simulate navigating back again
            currentWebsite = (string)browserHistory.Pop();
            Console.WriteLine("Current Website: " +
           currentWebsite);
            // Simulate adding a new website to the history
            browserHistory.Push("https://www.github.com");
            // Peek at the most recent website in the history without removing it
            string recentWebsite = (string)browserHistory.Peek();
            Console.WriteLine("Recent Website: " + recentWebsite);
            // Iterate over the browser history
            foreach (var website in browserHistory)
            {
                Console.WriteLine("Visited: " + website);
            }

            // Create a Hashtable to store student grades
            Hashtable studentGrades = new Hashtable();
            // Add student names and corresponding grades to the Hashtable
            studentGrades.Add("Alice", 95);
            studentGrades.Add("Bob", 87);
            studentGrades.Add("Charlie", 92);
            studentGrades.Add("Diana", 78);
            // Access and display the grade of a specific student
            int bobGrade = (int)studentGrades["Bob"];
            Console.WriteLine("Bob's Grade: " + bobGrade);
            // Modify the grade of a student
            studentGrades["Charlie"] = 88;
            // Check if a student is present in the Hashtable
            bool hasAlice = studentGrades.ContainsKey("Alice");
            Console.WriteLine("Has Alice: " + hasAlice);
            // Remove a student and their grade from the Hashtable
            studentGrades.Remove("Diana");
            // Iterate over the Hashtable
            foreach (DictionaryEntry entry in studentGrades)
            {
                string studentName = (string)entry.Key;
                int grade = (int)entry.Value;
                Console.WriteLine(studentName + ": " +
               grade);
            }


            SortedList sortedList = new SortedList();
            // Adding key-value pairs to the sorted list
            sortedList.Add(3, "Apple");
            sortedList.Add(1, "Banana");
            sortedList.Add(2, "Orange");
            // Accessing elements in the sorted list
            Console.WriteLine(sortedList[1]);
            // Iterating over the sorted list
            foreach (DictionaryEntry entry in sortedList)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
            // Removing elements from the sorted list
            sortedList.Remove(2);
            // Checking if a key exists in the sorted list
            bool containsKey = sortedList.ContainsKey(2); //false
            // Checking if a value exists in the sorted list
            bool containsValue = sortedList.ContainsValue("Apple");

            List<Person> people = new List<Person>();
            // Adding people to the list
            people.Add(new Person("Alice", 25));
            people.Add(new Person("Bob", 30));
            people.Add(new Person("Charlie", 35));
            Console.WriteLine("People:");
            foreach (Person person in people)
            {
                Console.WriteLine(person);
            }
            // Modifying a person's age
            people[1].Age = 32;
            // Checking if a person exists in the list
            bool contains = people.Contains(new Person("Alice", 25)); // true
                                                                      // Sorting the people by age
            people.Sort((p1, p2) => p1.Age.CompareTo(p2.Age));
            // Displaying the sorted people
            Console.WriteLine("\nSorted People (by Age):");
            foreach (Person person in people)
            {
                Console.WriteLine(person);
            }

            Dictionary<string, int> studentGrade = new Dictionary<string, int>();
            // Adding student names and grades to the dictionary
            studentGrade.Add("Alice", 90);
            studentGrade.Add("Bob", 85);
            studentGrade.Add("Charlie", 92);
            // Accessing grades in the dictionary
            Console.WriteLine("Bob's Grade: " + studentGrade["Bob"]);
            studentGrades["Alice"] = 95;
            // Checking if a student exists in the dictionary
            bool containsStudent = studentGrades.ContainsKey("Charlie"); // true
                                                                         // Removing students from the dictionary
            studentGrades.Remove("Bob");
            // Iterating over the dictionary
            foreach (KeyValuePair<string, int> entry in studentGrade)
            {
                Console.WriteLine("Name: " + entry.Key + ", sGrade: " + entry.Value);
            }

            SortedList<DateTime, Task> tasksByDeadline = new SortedList<DateTime, Task>();
            // Adding tasks to the sorted list
            tasksByDeadline.Add(DateTime.Parse("2023-06-25"), new Task("Complete Report", DateTime.Parse("2023-06-25")));
            tasksByDeadline.Add(DateTime.Parse("2023-06-20"), new Task("Submit Proposal", DateTime.Parse("2023-06-20")));

            // Accessing tasks in the sorted list
            Console.WriteLine("Task due on June 20th: " + tasksByDeadline[DateTime.Parse("2023-06-20")]);
            // Output: Submit Proposal: 6/20/2023
            // Removing tasks from the sorted list
            tasksByDeadline.Remove(DateTime.Parse("2023-06-20"));
            // Iterating over the sorted list
            foreach (KeyValuePair<DateTime, Task> kvp in tasksByDeadline)
            {
                Console.WriteLine(kvp.Value);
            }


            Queue<Order> orderQueue = new Queue<Order>();
            // Adding orders to the queue
            orderQueue.Enqueue(new Order(1, "Alice", 99.99m));
            orderQueue.Enqueue(new Order(2, "Bob", 49.99m));
            orderQueue.Enqueue(new Order(3, "Charlie", 149.99m));

            // Iterating over the queue
            foreach (Order order in orderQueue)
            {
                Console.WriteLine(order);
            }

            Stack<Book> bookStack = new Stack<Book>();
            // Adding books to the stack
            bookStack.Push(new Book(1, "The Great Gatsby","F. Scott Fitzgerald"));
            bookStack.Push(new Book(2, "To Kill a Mockingbird", "Harper Lee"));
           
            bookStack.Push(new Book(3, "1984", "George Orwell"));
            // Iterating over the stack
            foreach (Book book in bookStack)
            {
                Console.WriteLine(book);
            }
        }
    }
}
