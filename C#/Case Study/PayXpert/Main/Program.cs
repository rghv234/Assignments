using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayXpert.DAO;
using PayXpert.Entities;

namespace PayXpert
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting application...");

                // Example usage of services
                var employeeService = new EmployeeService();
                var employee = new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Gender = "Male",
                    PhoneNumber = "123-456-7890",
                    Address = "123 Main St", // Required since address is NOT NULL
                    Position = "Software Engineer", // Required since position is NOT NULL
                    JoiningDate = new DateTime(2020, 1, 1),
                };

                // Add employee
                int newEmployeeId = employeeService.AddEmployee(employee);

                // Retrieve employee
                var retrievedEmployee = employeeService.GetEmployeeById(newEmployeeId);
                if (retrievedEmployee != null)
                {
                    Console.WriteLine($"Retrieved Employee: {retrievedEmployee.FirstName} {retrievedEmployee.LastName}, Age: {retrievedEmployee.CalculateAge()}");
                }
                else
                {
                    Console.WriteLine("Failed to retrieve employee.");
                }

                // Generate payroll (example)
                var payrollService = new PayrollService();
                var payroll = payrollService.GeneratePayroll(newEmployeeId, new DateTime(2025, 4, 1), new DateTime(2025, 4, 15));
                Console.WriteLine($"Generated Payroll: Net Salary = {payroll.NetSalary}");

                // Clean up (optional, for demo)
                employeeService.RemoveEmployee(newEmployeeId);
                Console.WriteLine("Employee removed successfully.");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
    }
}
