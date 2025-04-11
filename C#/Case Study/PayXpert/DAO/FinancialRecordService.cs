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
}
