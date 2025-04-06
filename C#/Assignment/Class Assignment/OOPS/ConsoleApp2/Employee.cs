using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Employee
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return $"{this.Name}, {this.Code}, {this.Email}";
        }
    }

    internal class OverrideObj
    {
        static void Main()
        {
            Employee employee1 = new Employee();
            Console.WriteLine(employee1.ToString());

            int num = 10;
            string str = num.ToString();//it will convert int to string
            Console.WriteLine(str);

            //overriden
            Employee emp1 = new Employee { Code = 100, Name = "scott", Email = "scott@gmail.com" };
            Employee emp2 = new Employee { Code = 101, Name = "tiger", Email = "tiger@gmail.com" };

            //emp1 is this and emp2 is parameter
            var result1 = emp1.Equals(emp2);
            Console.WriteLine(result1 ? "both are same employee" : "both are different employees");
        }

    }
}
