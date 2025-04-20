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
                string query = "INSERT INTO payroll (employeeid, payperiodstartdate, payperiodenddate, basicsalary, overtimepay, deductions, netsalary) " +
                               "OUTPUT INSERTED.payrollid VALUES (@EmployeeId, @StartDate, @EndDate, @BasicSalary, @OvertimePay, @Deductions, @NetSalary)";
                SqlParameter[] parameters = {
                    new SqlParameter("@EmployeeId", employeeId),
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
}
