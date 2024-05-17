using PayXpert.model;
using PayXpert.util;
using System.Data.SqlClient;

namespace PayXpert.service
{
    public class FinancialRecordService : IFinancialRecordService
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public FinancialRecordService()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }
        public void AddFinancialRecord(int employeeId, string description, double amount, string recordType)
        {
            try
            {
                sqlConnection.Open();
                if (employeeId <= 0 || string.IsNullOrWhiteSpace(description) || amount <= 0 || string.IsNullOrWhiteSpace(recordType))
                {
                    throw new ArgumentException("Invalid financial record input.");
                }

                cmd.CommandText = "INSERT INTO FinancialRecord (EmployeeID, RecordDate, Description, Amount, RecordType) " +
                               "VALUES (@EmployeeID, @RecordDate, @Description, @Amount, @RecordType)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Parameters.AddWithValue("@RecordDate", DateTime.Now); // Assuming record date is current date/time
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@RecordType", recordType);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error Adding Financial Record: " + ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "SELECT * FROM FinancialRecord WHERE RecordId = @RecordId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RecordId", recordId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    FinancialRecord record = new FinancialRecord
                    {
                        RecordId = Convert.ToInt32(reader["RecordId"]),
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                        Description = reader["Description"].ToString(),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        RecordType = reader["RecordType"].ToString()
                    };
                    return record;
                }
                return null;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error Getting Financial Record: " + ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            try
            {
                sqlConnection.Open();
                List<FinancialRecord> records = new List<FinancialRecord>();
                cmd.CommandText = "SELECT * FROM FinancialRecord WHERE RecordDate = @RecordDate";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RecordDate", recordDate);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FinancialRecord record = new FinancialRecord
                    {
                        RecordId = Convert.ToInt32(reader["RecordId"]),
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                        Description = reader["Description"].ToString(),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        RecordType = reader["RecordType"].ToString()
                    };

                    records.Add(record);
                }
                return records;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error getting Financial Records for employee: " + ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            try
            {
                sqlConnection.Open();
                List<FinancialRecord> financialRecords = new List<FinancialRecord>();
                cmd.CommandText = "SELECT * FROM FinancialRecord WHERE EmployeeId = @EmployeeId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FinancialRecord record = new FinancialRecord
                    {
                        RecordId = Convert.ToInt32(reader["RecordId"]),
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                        Description = reader["Description"].ToString(),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        RecordType = reader["RecordType"].ToString()
                    };
                    financialRecords.Add(record);
                }
                return financialRecords;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error getting Financial Records for employee: " + ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
