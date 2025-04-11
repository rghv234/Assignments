using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayXpert.Entities;
using PayXpert.Exception;

namespace PayXpert.DAO
{
    public class EmployeeService : IEmployeeService
    {
        public Employee GetEmployeeById(int employeeId)
        {
            using (var db = new DatabaseContext())
            {
                string query = "SELECT employeeid, firstname, lastname, dateofbirth, gender, email, phonenumber, address, position, joiningdate, terminationdate FROM employee WHERE employeeid = @EmployeeID";
                SqlParameter[] parameters = { new SqlParameter("@EmployeeID", employeeId) };
                using (var reader = db.ExecuteQuery(query, parameters))
                {
                    if (reader.Read())
                    {
                        // Debug output to identify NULL values
                        Console.WriteLine($"Debug - firstname: {reader.IsDBNull(reader.GetOrdinal("firstname"))}, lastname: {reader.IsDBNull(reader.GetOrdinal("lastname"))}, gender: {reader.IsDBNull(reader.GetOrdinal("gender"))}, email: {reader.IsDBNull(reader.GetOrdinal("email"))}, address: {reader.IsDBNull(reader.GetOrdinal("address"))}, position: {reader.IsDBNull(reader.GetOrdinal("position"))}, terminationdate: {reader.IsDBNull(reader.GetOrdinal("terminationdate"))}");

                        return new Employee(
                            reader.GetInt32(reader.GetOrdinal("employeeid")),
                            reader.IsDBNull(reader.GetOrdinal("firstname")) ? null : reader.GetString(reader.GetOrdinal("firstname")),
                            reader.IsDBNull(reader.GetOrdinal("lastname")) ? null : reader.GetString(reader.GetOrdinal("lastname")),
                            reader.GetDateTime(reader.GetOrdinal("dateofbirth")),
                            reader.IsDBNull(reader.GetOrdinal("gender")) ? null : reader.GetString(reader.GetOrdinal("gender")),
                            reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("phonenumber")),
                            reader.GetString(reader.GetOrdinal("address")), // Removed null check since address is NOT NULL
                            reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString(reader.GetOrdinal("position")),
                            reader.GetDateTime(reader.GetOrdinal("joiningdate")),
                            reader.GetDateTime(reader.GetOrdinal("terminationdate")) // Removed null check since terminationdate is NOT NULL
                        );
                    }
                    throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
                }
            }
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (var db = new DatabaseContext())
            {
                string query = "SELECT employeeid, firstname, lastname, dateofbirth, gender, email, phonenumber, address, position, joiningdate, terminationdate FROM employee";
                using (var reader = db.ExecuteQuery(query))
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee(
                            reader.GetInt32(reader.GetOrdinal("employeeid")),
                            reader.IsDBNull(reader.GetOrdinal("firstname")) ? null : reader.GetString(reader.GetOrdinal("firstname")),
                            reader.IsDBNull(reader.GetOrdinal("lastname")) ? null : reader.GetString(reader.GetOrdinal("lastname")),
                            reader.GetDateTime(reader.GetOrdinal("dateofbirth")),
                            reader.IsDBNull(reader.GetOrdinal("gender")) ? null : reader.GetString(reader.GetOrdinal("gender")),
                            reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("phonenumber")),
                            reader.GetString(reader.GetOrdinal("address")), // Removed null check since address is NOT NULL
                            reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString(reader.GetOrdinal("position")),
                            reader.GetDateTime(reader.GetOrdinal("joiningdate")),
                            reader.GetDateTime(reader.GetOrdinal("terminationdate")) // Removed null check since terminationdate is NOT NULL
                        ));
                    }
                }
            }
            return employees;
        }

        public int AddEmployee(Employee employeeData)
        {
            using (var db = new DatabaseContext())
            {
                // Check the last used employeeid and increment if IDENTITY is not working
                int newEmployeeId = GetNextEmployeeId(db);
                string query = "INSERT INTO employee (employeeid, firstname, lastname, dateofbirth, gender, email, phonenumber, address, position, joiningdate, terminationdate) " +
                               "OUTPUT INSERTED.employeeid VALUES (@EmployeeID, @FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position, @JoiningDate, @TerminationDate)";
                SqlParameter[] parameters = {
                    new SqlParameter("@EmployeeID", newEmployeeId),
                    new SqlParameter("@FirstName", (object)employeeData.FirstName ?? DBNull.Value),
                    new SqlParameter("@LastName", (object)employeeData.LastName ?? DBNull.Value),
                    new SqlParameter("@DateOfBirth", employeeData.DateOfBirth),
                    new SqlParameter("@Gender", (object)employeeData.Gender ?? DBNull.Value),
                    new SqlParameter("@Email", (object)employeeData.Email ?? DBNull.Value),
                    new SqlParameter("@PhoneNumber", employeeData.PhoneNumber),
                    new SqlParameter("@Address", employeeData.Address ?? "N/A"), // Default value since address is NOT NULL
                    new SqlParameter("@Position", employeeData.Position ?? "N/A"), // Default value since position is NOT NULL
                    new SqlParameter("@JoiningDate", employeeData.JoiningDate),
                    new SqlParameter("@TerminationDate", employeeData.TerminationDate ?? new DateTime(9999, 12, 31)) // Default value since terminationdate is NOT NULL
                };
                try
                {
                    using (var command = new SqlCommand(query, db.Connection))
                    {
                        command.Parameters.AddRange(parameters);
                        int insertedEmployeeId = (int)command.ExecuteScalar(); // Retrieve the inserted ID
                        Console.WriteLine($"Employee inserted successfully with ID {insertedEmployeeId}");
                        return insertedEmployeeId;
                    }
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException($"Database error during insertion: {ex.Message}", ex);
                }
            }
        }

        private int GetNextEmployeeId(DatabaseContext db)
        {
            string query = "SELECT ISNULL(MAX(employeeid), 0) + 1 FROM employee";
            using (var command = new SqlCommand(query, db.Connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateEmployee(Employee employeeData)
        {
            using (var db = new DatabaseContext())
            {
                string query = "UPDATE employee SET firstname = @FirstName, lastname = @LastName, dateofbirth = @DateOfBirth, " +
                               "gender = @Gender, email = @Email, phonenumber = @PhoneNumber, address = @Address, " +
                               "position = @Position, joiningdate = @JoiningDate, terminationdate = @TerminationDate " +
                               "WHERE employeeid = @EmployeeID";
                SqlParameter[] parameters = {
                    new SqlParameter("@FirstName", (object)employeeData.FirstName ?? DBNull.Value),
                    new SqlParameter("@LastName", (object)employeeData.LastName ?? DBNull.Value),
                    new SqlParameter("@DateOfBirth", employeeData.DateOfBirth),
                    new SqlParameter("@Gender", (object)employeeData.Gender ?? DBNull.Value),
                    new SqlParameter("@Email", (object)employeeData.Email ?? DBNull.Value),
                    new SqlParameter("@PhoneNumber", employeeData.PhoneNumber),
                    new SqlParameter("@Address", employeeData.Address ?? "N/A"),
                    new SqlParameter("@Position", employeeData.Position ?? "N/A"),
                    new SqlParameter("@JoiningDate", employeeData.JoiningDate),
                    new SqlParameter("@TerminationDate", employeeData.TerminationDate ?? new DateTime(9999, 12, 31)),
                    new SqlParameter("@EmployeeID", employeeData.EmployeeID)
                };
                int rowsAffected = db.ExecuteNonQuery(query, parameters);
                if (rowsAffected == 0)
                    throw new EmployeeNotFoundException($"Employee with ID {employeeData.EmployeeID} not found.");
            }
        }

        public void RemoveEmployee(int employeeId)
        {
            using (var db = new DatabaseContext())
            {
                // Delete related payroll records first
                string deletePayrollQuery = "DELETE FROM payroll WHERE employeeid = @EmployeeID";
                SqlParameter[] payrollParameters = { new SqlParameter("@EmployeeID", employeeId) };
                db.ExecuteNonQuery(deletePayrollQuery, payrollParameters);

                // Delete the employee
                string deleteEmployeeQuery = "DELETE FROM employee WHERE employeeid = @EmployeeID";
                SqlParameter[] employeeParameters = { new SqlParameter("@EmployeeID", employeeId) };
                int rowsAffected = db.ExecuteNonQuery(deleteEmployeeQuery, employeeParameters);
                if (rowsAffected == 0)
                    throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
            }
        }
    }

}