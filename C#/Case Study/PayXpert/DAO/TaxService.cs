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
}
