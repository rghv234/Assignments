using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayXpert.Entities;

namespace PayXpert.DAO
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployees();
        int AddEmployee(Employee employeeData); // Changed to return the generated employeeID
        void UpdateEmployee(Employee employeeData);
        void RemoveEmployee(int employeeId);
    }
}
