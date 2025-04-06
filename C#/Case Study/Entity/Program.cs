using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Entity
{
    // Model/Entity Classes
    public class Employee
    {
        private int employeeID;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string gender;
        private string email;
        private string phoneNumber;
        private string address;
        private string position;
        private DateTime joiningDate;
        private DateTime? terminationDate;

        public Employee() { }

        public Employee(int employeeID, string firstName, string lastName, DateTime dateOfBirth,
            string gender, string email, string phoneNumber, string address, string position,
            DateTime joiningDate, DateTime? terminationDate)
        {
            this.employeeID = employeeID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.position = position;
            this.joiningDate = joiningDate;
            this.terminationDate = terminationDate;
        }

        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Address { get => address; set => address = value; }
        public string Position { get => position; set => position = value; }
        public DateTime JoiningDate { get => joiningDate; set => joiningDate = value; }
        public DateTime? TerminationDate { get => terminationDate; set => terminationDate = value; }

        public int CalculateAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public class Payroll
    {
        private int payrollID;
        private int employeeID;
        private DateTime payPeriodStartDate;
        private DateTime payPeriodEndDate;
        private decimal basicSalary;
        private decimal overtimePay;
        private decimal deductions;
        private decimal netSalary;

        public Payroll() { }

        public Payroll(int payrollID, int employeeID, DateTime payPeriodStartDate,
            DateTime payPeriodEndDate, decimal basicSalary, decimal overtimePay,
            decimal deductions, decimal netSalary)
        {
            this.payrollID = payrollID;
            this.employeeID = employeeID;
            this.payPeriodStartDate = payPeriodStartDate;
            this.payPeriodEndDate = payPeriodEndDate;
            this.basicSalary = basicSalary;
            this.overtimePay = overtimePay;
            this.deductions = deductions;
            this.netSalary = netSalary;
        }

        public int PayrollID { get => payrollID; set => payrollID = value; }
        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public DateTime PayPeriodStartDate { get => payPeriodStartDate; set => payPeriodStartDate = value; }
        public DateTime PayPeriodEndDate { get => payPeriodEndDate; set => payPeriodEndDate = value; }
        public decimal BasicSalary { get => basicSalary; set => basicSalary = value; }
        public decimal OvertimePay { get => overtimePay; set => overtimePay = value; }
        public decimal Deductions { get => deductions; set => deductions = value; }
        public decimal NetSalary { get => netSalary; set => netSalary = value; }
    }

    public class Tax
    {
        private int taxID;
        private int employeeID;
        private int taxYear;
        private decimal taxableIncome;
        private decimal taxAmount;

        public Tax() { }

        public Tax(int taxID, int employeeID, int taxYear, decimal taxableIncome, decimal taxAmount)
        {
            this.taxID = taxID;
            this.employeeID = employeeID;
            this.taxYear = taxYear;
            this.taxableIncome = taxableIncome;
            this.taxAmount = taxAmount;
        }

        public int TaxID { get => taxID; set => taxID = value; }
        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public int TaxYear { get => taxYear; set => taxYear = value; }
        public decimal TaxableIncome { get => taxableIncome; set => taxableIncome = value; }
        public decimal TaxAmount { get => taxAmount; set => taxAmount = value; }
    }

    public class FinancialRecord
    {
        private int recordID;
        private int employeeID;
        private DateTime recordDate;
        private string description;
        private decimal amount;
        private string recordType;

        public FinancialRecord() { }

        public FinancialRecord(int recordID, int employeeID, DateTime recordDate,
            string description, decimal amount, string recordType)
        {
            this.recordID = recordID;
            this.employeeID = employeeID;
            this.recordDate = recordDate;
            this.description = description;
            this.amount = amount;
            this.recordType = recordType;
        }

        public int RecordID { get => recordID; set => recordID = value; }
        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public DateTime RecordDate { get => recordDate; set => recordDate = value; }
        public string Description { get => description; set => description = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public string RecordType { get => recordType; set => recordType = value; }
    }

    // Interfaces
    public interface IEmployeeService
    {
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployees();
        int AddEmployee(Employee employeeData); // Changed to return the generated employeeID
        void UpdateEmployee(Employee employeeData);
        void RemoveEmployee(int employeeId);
    }

    public interface IPayrollService
    {
        Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate);
        Payroll GetPayrollById(int payrollId);
        List<Payroll> GetPayrollsForEmployee(int employeeId);
        List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
    }

    public interface ITaxService
    {
        Tax CalculateTax(int employeeId, int taxYear);
        Tax GetTaxById(int taxId);
        List<Tax> GetTaxesForEmployee(int employeeId);
        List<Tax> GetTaxesForYear(int taxYear);
    }

    public interface IFinancialRecordService
    {
        void AddFinancialRecord(int employeeId, string description, decimal amount, string recordType);
        FinancialRecord GetFinancialRecordById(int recordId);
        List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId);
        List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate);
    }

    // Service Classes with Implementations
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

    public class PayrollService : IPayrollService
    {
        public Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate)
        {
            using (var db = new DatabaseContext())
            {
                decimal basicSalary = 5000m;
                decimal overtimePay = 500m;
                decimal deductions = 200m;
                decimal netSalary = basicSalary + overtimePay - deductions;

                // Check the last used payrollid and increment if IDENTITY is not working
                int newPayrollId = GetNextPayrollId(db);
                string query = "INSERT INTO payroll (payrollid, employeeid, payperiodstartdate, payperiodenddate, basicsalary, overtimepay, deductions, netsalary) " +
                               "OUTPUT INSERTED.payrollid VALUES (@PayrollID, @EmployeeID, @StartDate, @EndDate, @BasicSalary, @OvertimePay, @Deductions, @NetSalary)";
                SqlParameter[] parameters = {
                    new SqlParameter("@PayrollID", newPayrollId),
                    new SqlParameter("@EmployeeID", employeeId),
                    new SqlParameter("@StartDate", startDate),
                    new SqlParameter("@EndDate", endDate),
                    new SqlParameter("@BasicSalary", basicSalary),
                    new SqlParameter("@OvertimePay", overtimePay),
                    new SqlParameter("@Deductions", deductions),
                    new SqlParameter("@NetSalary", netSalary)
                };
                try
                {
                    using (var command = new SqlCommand(query, db.Connection))
                    {
                        command.Parameters.AddRange(parameters);
                        int payrollId = (int)command.ExecuteScalar();
                        return new Payroll(payrollId, employeeId, startDate, endDate, basicSalary, overtimePay, deductions, netSalary);
                    }
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.", ex);
                }
            }
        }

        private int GetNextPayrollId(DatabaseContext db)
        {
            string query = "SELECT ISNULL(MAX(payrollid), 0) + 1 FROM payroll";
            using (var command = new SqlCommand(query, db.Connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        public Payroll GetPayrollById(int payrollId)
        {
            using (var db = new DatabaseContext())
            {
                string query = "SELECT * FROM payroll WHERE payrollid = @PayrollID";
                SqlParameter[] parameters = { new SqlParameter("@PayrollID", payrollId) };
                using (var reader = db.ExecuteQuery(query, parameters))
                {
                    if (reader.Read())
                    {
                        return new Payroll(
                            reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2),
                            reader.GetDateTime(3), reader.GetDecimal(4), reader.GetDecimal(5),
                            reader.GetDecimal(6), reader.GetDecimal(7));
                    }
                    throw new PayrollGenerationException($"Payroll with ID {payrollId} not found.");
                }
            }
        }

        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            List<Payroll> payrolls = new List<Payroll>();
            using (var db = new DatabaseContext())
            {
                string query = "SELECT * FROM payroll WHERE employeeid = @EmployeeID";
                SqlParameter[] parameters = { new SqlParameter("@EmployeeID", employeeId) };
                using (var reader = db.ExecuteQuery(query, parameters))
                {
                    while (reader.Read())
                    {
                        payrolls.Add(new Payroll(
                            reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2),
                            reader.GetDateTime(3), reader.GetDecimal(4), reader.GetDecimal(5),
                            reader.GetDecimal(6), reader.GetDecimal(7)));
                    }
                }
            }
            return payrolls;
        }

        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrolls = new List<Payroll>();
            using (var db = new DatabaseContext())
            {
                string query = "SELECT * FROM payroll WHERE payperiodstartdate >= @StartDate AND payperiodenddate <= @EndDate";
                SqlParameter[] parameters = {
                    new SqlParameter("@StartDate", startDate),
                    new SqlParameter("@EndDate", endDate)
                };
                using (var reader = db.ExecuteQuery(query, parameters))
                {
                    while (reader.Read())
                    {
                        payrolls.Add(new Payroll(
                            reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2),
                            reader.GetDateTime(3), reader.GetDecimal(4), reader.GetDecimal(5),
                            reader.GetDecimal(6), reader.GetDecimal(7)));
                    }
                }
            }
            return payrolls;
        }
    }

    public class TaxService : ITaxService
    {
        public Tax CalculateTax(int employeeId, int taxYear)
        {
            using (var db = new DatabaseContext())
            {
                decimal taxableIncome = 60000m;
                decimal taxAmount = taxableIncome * 0.2m;

                string query = "INSERT INTO tax (employeeid, taxyear, taxableincome, taxamount) " +
                               "OUTPUT INSERTED.taxid VALUES (@EmployeeID, @TaxYear, @TaxableIncome, @TaxAmount)";
                SqlParameter[] parameters = {
                    new SqlParameter("@EmployeeID", employeeId),
                    new SqlParameter("@TaxYear", taxYear),
                    new SqlParameter("@TaxableIncome", taxableIncome),
                    new SqlParameter("@TaxAmount", taxAmount)
                };
                try
                {
                    using (var command = new SqlCommand(query, db.Connection))
                    {
                        command.Parameters.AddRange(parameters);
                        int taxId = (int)command.ExecuteScalar();
                        return new Tax(taxId, employeeId, taxYear, taxableIncome, taxAmount);
                    }
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.", ex);
                }
            }
        }

        public Tax GetTaxById(int taxId)
        {
            using (var db = new DatabaseContext())
            {
                string query = "SELECT * FROM tax WHERE taxid = @TaxID";
                SqlParameter[] parameters = { new SqlParameter("@TaxID", taxId) };
                using (var reader = db.ExecuteQuery(query, parameters))
                {
                    if (reader.Read())
                    {
                        return new Tax(
                            reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),
                            reader.GetDecimal(3), reader.GetDecimal(4));
                    }
                    throw new TaxCalculationException($"Tax with ID {taxId} not found.");
                }
            }
        }

        public List<Tax> GetTaxesForEmployee(int employeeId)
        {
            List<Tax> taxes = new List<Tax>();
            using (var db = new DatabaseContext())
            {
                string query = "SELECT * FROM tax WHERE employeeid = @EmployeeID";
                SqlParameter[] parameters = { new SqlParameter("@EmployeeID", employeeId) };
                using (var reader = db.ExecuteQuery(query, parameters))
                {
                    while (reader.Read())
                    {
                        taxes.Add(new Tax(
                            reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),
                            reader.GetDecimal(3), reader.GetDecimal(4)));
                    }
                }
            }
            return taxes;
        }

        public List<Tax> GetTaxesForYear(int taxYear)
        {
            List<Tax> taxes = new List<Tax>();
            using (var db = new DatabaseContext())
            {
                string query = "SELECT * FROM tax WHERE taxyear = @TaxYear";
                SqlParameter[] parameters = { new SqlParameter("@TaxYear", taxYear) };
                using (var reader = db.ExecuteQuery(query, parameters))
                {
                    while (reader.Read())
                    {
                        taxes.Add(new Tax(
                            reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),
                            reader.GetDecimal(3), reader.GetDecimal(4)));
                    }
                }
            }
            return taxes;
        }
    }

    public class FinancialRecordService : IFinancialRecordService
    {
        public void AddFinancialRecord(int employeeId, string description, decimal amount, string recordType)
        {
            using (var db = new DatabaseContext())
            {
                string query = "INSERT INTO financialrecord (employeeid, recorddate, description, amount, recordtype) " +
                               "OUTPUT INSERTED.recordid VALUES (@EmployeeID, @RecordDate, @Description, @Amount, @RecordType)";
                SqlParameter[] parameters = {
                    new SqlParameter("@EmployeeID", employeeId),
                    new SqlParameter("@RecordDate", DateTime.Now),
                    new SqlParameter("@Description", (object)description ?? DBNull.Value),
                    new SqlParameter("@Amount", amount),
                    new SqlParameter("@RecordType", (object)recordType ?? DBNull.Value)
                };
                try
                {
                    using (var command = new SqlCommand(query, db.Connection))
                    {
                        command.Parameters.AddRange(parameters);
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.", ex);
                }
            }
        }

        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            using (var db = new DatabaseContext())
            {
                string query = "SELECT * FROM financialrecord WHERE recordid = @RecordID";
                SqlParameter[] parameters = { new SqlParameter("@RecordID", recordId) };
                using (var reader = db.ExecuteQuery(query, parameters))
                {
                    if (reader.Read())
                    {
                        return new FinancialRecord(
                            reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2),
                            reader.IsDBNull(3) ? null : reader.GetString(3), reader.GetDecimal(4),
                            reader.IsDBNull(5) ? null : reader.GetString(5));
                    }
                    throw new FinancialRecordException($"Financial record with ID {recordId} not found.");
                }
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            List<FinancialRecord> records = new List<FinancialRecord>();
            using (var db = new DatabaseContext())
            {
                string query = "SELECT * FROM financialrecord WHERE employeeid = @EmployeeID";
                SqlParameter[] parameters = { new SqlParameter("@EmployeeID", employeeId) };
                using (var reader = db.ExecuteQuery(query, parameters))
                {
                    while (reader.Read())
                    {
                        records.Add(new FinancialRecord(
                            reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2),
                            reader.IsDBNull(3) ? null : reader.GetString(3), reader.GetDecimal(4),
                            reader.IsDBNull(5) ? null : reader.GetString(5)));
                    }
                }
            }
            return records;
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            List<FinancialRecord> records = new List<FinancialRecord>();
            using (var db = new DatabaseContext())
            {
                string query = "SELECT * FROM financialrecord WHERE recorddate = @RecordDate";
                SqlParameter[] parameters = { new SqlParameter("@RecordDate", recordDate) };
                using (var reader = db.ExecuteQuery(query, parameters))
                {
                    while (reader.Read())
                    {
                        records.Add(new FinancialRecord(
                            reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2),
                            reader.IsDBNull(3) ? null : reader.GetString(3), reader.GetDecimal(4),
                            reader.IsDBNull(5) ? null : reader.GetString(5)));
                    }
                }
            }
            return records;
        }
    }

    // Database Context
    public class DatabaseContext : IDisposable
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public DatabaseContext()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=payxpert;Trusted_Connection=True;";
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection Connection
        {
            get
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (SqlException ex)
                    {
                        throw new DatabaseConnectionException("Failed to establish database connection.", ex);
                    }
                }
                return connection;
            }
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error executing database command.", ex);
                }
            }
        }

        public SqlDataReader ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            SqlCommand command = new SqlCommand(query, Connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            try
            {
                return command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error executing database query.", ex);
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
        }
    }

    // Additional Classes
    public class ValidationService
    {
        public ValidationService()
        {
            // Initialize validation service
        }
    }

    public class ReportGenerator
    {
        public ReportGenerator()
        {
            // Initialize report generator
        }
    }

    // Custom Exceptions
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException() : base("Employee not found.") { }
        public EmployeeNotFoundException(string message) : base(message) { }
        public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class PayrollGenerationException : Exception
    {
        public PayrollGenerationException() : base("Error generating payroll.") { }
        public PayrollGenerationException(string message) : base(message) { }
        public PayrollGenerationException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TaxCalculationException : Exception
    {
        public TaxCalculationException() : base("Error calculating tax.") { }
        public TaxCalculationException(string message) : base(message) { }
        public TaxCalculationException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class FinancialRecordException : Exception
    {
        public FinancialRecordException() : base("Error managing financial record.") { }
        public FinancialRecordException(string message) : base(message) { }
        public FinancialRecordException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidInputException : Exception
    {
        public InvalidInputException() : base("Invalid input data provided.") { }
        public InvalidInputException(string message) : base(message) { }
        public InvalidInputException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException() : base("Database connection error.") { }
        public DatabaseConnectionException(string message) : base(message) { }
        public DatabaseConnectionException(string message, Exception innerException) : base(message, innerException) { }
    }

    // Entry Point
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
                    TerminationDate = new DateTime(9999, 12, 31) // Required since terminationdate is NOT NULL, set to far future for active employees
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
            catch (Exception ex)
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