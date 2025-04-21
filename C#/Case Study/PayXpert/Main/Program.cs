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

                // Create services
                var employeeService = new EmployeeService();
                var payrollService = new PayrollService();

                // Get employee details from user
                Console.WriteLine("\nEnter Employee Details:");
                Console.Write("First Name: ");
                string firstName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(firstName)) firstName = "Unknown";

                Console.Write("Last Name: ");
                string lastName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(lastName)) lastName = "Unknown";

                Console.Write("Date of Birth (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                {
                    Console.WriteLine("Invalid date format. Using default: 1990-01-01.");
                    dateOfBirth = new DateTime(1990, 1, 1);
                }

                Console.Write("Gender: ");
                string gender = Console.ReadLine()?.Trim() ?? "Not Specified";

                Console.Write("Phone Number: ");
                string phoneNumber = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(phoneNumber)) phoneNumber = "000-000-0000";

                Console.Write("Address: ");
                string address = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(address)) address = "N/A"; // Required, provide default

                Console.Write("Position: ");
                string position = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(position)) position = "N/A"; // Required, provide default

                Console.Write("Joining Date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime joiningDate))
                {
                    Console.WriteLine("Invalid date format. Using default: 2020-01-01.");
                    joiningDate = new DateTime(2020, 1, 1);
                }

                Console.Write("Termination Date (yyyy-mm-dd, or press Enter for null): ");
                DateTime? terminationDate = null;
                string termDateInput = Console.ReadLine()?.Trim();
                DateTime tempTermDate; // Declare outside
                if (!string.IsNullOrEmpty(termDateInput) && DateTime.TryParse(termDateInput, out tempTermDate))
                {
                    terminationDate = tempTermDate;
                }
                else
                {
                    Console.WriteLine("Invalid date format or empty input. Using null for termination date.");
                }

                // Create employee object
                var employee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Gender = gender,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Position = position,
                    JoiningDate = joiningDate,
                    TerminationDate = terminationDate
                };

                // Add employee
                int newEmployeeId = employeeService.AddEmployee(employee);
                Console.WriteLine($"\nEmployee added successfully with ID {newEmployeeId}");

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

                // Get payroll period from user
                Console.WriteLine("\nEnter Payroll Period:");
                Console.Write("Start Date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    Console.WriteLine("Invalid date format. Using default: 2025-04-01.");
                    startDate = new DateTime(2025, 4, 1);
                }

                Console.Write("End Date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    Console.WriteLine("Invalid date format. Using default: 2025-04-15.");
                    endDate = new DateTime(2025, 4, 15);
                }

                // Generate payroll
                var payroll = payrollService.GeneratePayroll(newEmployeeId, startDate, endDate);
                Console.WriteLine($"Generated Payroll: Net Salary = {payroll.NetSalary}");

                // Clean up (optional, for demo)
                employeeService.RemoveEmployee(newEmployeeId);
                Console.WriteLine("Employee removed successfully.");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
