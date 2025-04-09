using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {
        class InvalidEmployeeCode : Exception
        {
            public InvalidEmployeeCode() : base("Invalid Employee Code")
            {
            }
        }
        class Employee

        {
            public void ValidateEmployeeCode
           (int EmployeeCode
           )

            {
                if (EmployeeCode <= 0)

                {
                    throw new InvalidEmployeeCode();

                }
            }
        }
        static void Main(string[] args)
        {
            try

            {
                int a, b;
                a = Convert.ToInt32(Console.ReadLine());
                b = Convert.ToInt32(Console.ReadLine());
                int c = a / b;
                Console.WriteLine("Answer is :{0}", c);

            }
            catch (DivideByZeroException ex)

            {
                Console.WriteLine
               (ex.Message);

            }
            catch (FormatException ex)

            {
                Console.WriteLine
               (ex.Message);

            }
            finally

            {
                Console.WriteLine("I am in finally");
                GC.Collect();

            }
            int code = Convert.ToInt32(Console.ReadLine());
            Employee emp = new Employee();
            try

            {
                emp.ValidateEmployeeCode(code);

            }
            catch (InvalidEmployeeCode ex)

            {
                Console.WriteLine
               (ex.Message);

            }
            Console.ReadLine();
        }
    }
}
