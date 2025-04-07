using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorDestructor
{
    internal class Student
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public Student(int RollNo)
        {
            Console.WriteLine($"Student Overloaded Constrcutor:{ RollNo}");
        }
        public Student(int RollNo, string Name)
        {
            this.RollNo = RollNo;
            this.Name = Name;
        }

        public Student(Student student)
        {
            this.RollNo = student.RollNo;
            this.Name = student.Name;
        }
        //destructor function-invokes immediately after closing the application
        ~Student()
        {
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student(101);
            Console.WriteLine($"Student1 -> RollNo: {student1.RollNo}, Name: {student1.Name}");

            // Using parameterized constructor with RollNo and Name
            Student student2 = new Student(102, "Alice");
            Console.WriteLine($"Student2 -> RollNo: {student2.RollNo}, Name: {student2.Name}");

            // Using copy constructor to create student3 from student2
            Student student3 = new Student(student2);
            Console.WriteLine($"Student3 (Copy of Student2) -> RollNo: {student3.RollNo}, Name: {student3.Name}");

            // Keeping console open
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
