using PayXpert.exception;
using PayXpert.model;
using PayXpert.util;
using System.Data.SqlClient;

namespace PayXpert.service
{
    public class TaxService : ITaxService
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public TaxService()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }
        public void CalculateTax(int employeeId, int taxYear)
        {
            try

            {
                sqlConnection.Open();
                // Fetch taxable income from another source, such as user input or an external system
                decimal netSalary = FetchNetSalaryForEmployee(employeeId) * 12;

                if (netSalary <= 0)
                {
                    Console.WriteLine(new ArgumentException("Invalid taxable income."));
                }

                decimal taxAmount = TaxCalculationHelper.CalculateTaxAmount(netSalary);

                cmd.CommandText = "INSERT INTO Tax (EmployeeId, TaxYear, TaxableIncome, TaxAmount) " +
                                   "VALUES (@EmployeeId, @TaxYear, @TaxableIncome, @TaxAmount)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                cmd.Parameters.AddWithValue("@TaxableIncome", netSalary);
                cmd.Parameters.AddWithValue("@TaxAmount", taxAmount);
                cmd.ExecuteNonQuery();
            }
            catch (TaxCalculationException ex)
            {
                Console.WriteLine($"Error calculating Tax for employee. {ex.Message}");
                throw;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error calculating Tax: " + ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private decimal FetchNetSalaryForEmployee(int employeeId)
        {
            cmd.CommandText = "SELECT NetSalary FROM Payroll WHERE EmployeeId = @EmployeeId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            object result = cmd.ExecuteScalar();

            if (result != null && decimal.TryParse(result.ToString(), out decimal netSalary))
            {
                return netSalary;
            }

            // If net Salary is not found or not valid, return 0
            return 0;
        }
        public Tax GetTaxById(int taxId)
        {
            cmd.CommandText = "SELECT * FROM Tax WHERE TaxId = @TaxId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TaxId", taxId);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Tax tax = new Tax
                    {
                        TaxId = Convert.ToInt32(reader["TaxId"]),
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        TaxYear = Convert.ToInt32(reader["TaxYear"]),
                        TaxableIncome = Convert.ToDecimal(reader["TaxableIncome"]),
                        TaxAmount = Convert.ToDecimal(reader["TaxAmount"])
                    };
                    return tax;
                }
                else
                {
                    return null; // Tax not found
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<Tax> GetTaxesForEmployee(int employeeId)
        {
            try
            {
                List<Tax> taxs = new List<Tax>();
                sqlConnection.Open();
                cmd.CommandText = "SELECT * FROM Tax WHERE EmployeeId = @EmployeeId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tax tax = new Tax
                    {
                        TaxId = Convert.ToInt32(reader["TaxId"]),
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        TaxYear = Convert.ToInt32(reader["TaxYear"]),
                        TaxableIncome = Convert.ToDecimal(reader["TaxableIncome"]),
                        TaxAmount = Convert.ToDecimal(reader["TaxAmount"])
                    };
                    taxs.Add(tax);
                }
                return taxs;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error getting payrolls for employee: " + ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<Tax> GetTaxesForYear(int taxYear)
        {
            try
            {
                List<Tax> taxs = new List<Tax>();
                sqlConnection.Open();
                cmd.CommandText = "SELECT * FROM Tax WHERE TaxYear = @TaxYear";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tax tax = new Tax
                    {
                        TaxId = Convert.ToInt32(reader["TaxId"]),
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        TaxYear = Convert.ToInt32(reader["TaxYear"]),
                        TaxableIncome = Convert.ToDecimal(reader["TaxableIncome"]),
                        TaxAmount = Convert.ToDecimal(reader["TaxAmount"])
                    };
                    taxs.Add(tax);
                }
                return taxs;
            }
            catch (SqlException ex)
            {                
                Console.WriteLine("Error getting payrolls for period: " + ex.Message);             
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static class TaxCalculationHelper
        {
            public static decimal CalculateTaxAmount(decimal netSalary)
            {
                decimal taxRate = GetTaxRate(netSalary);
                decimal taxAmount = netSalary * taxRate - GetTaxRebate(netSalary);
                Console.WriteLine($"Calculated Tax : {taxAmount}");
                return taxAmount;
            }

            private static decimal GetTaxRate(decimal taxableIncome)
            {
                // Example tax rate calculation based on income thresholds
                if (taxableIncome <= 100000)
                {
                    return 0.1m; // 10% tax rate
                }
                else if (taxableIncome <= 500000)
                {
                    return 0.15m; // 15% tax rate
                }
                else
                {
                    return 0.2m; // 20% tax rate
                }
            }
            private static decimal GetTaxRebate(decimal netSalary)
            {
                if(netSalary <= 500000)
                {
                    return 12500;
                }
                else if(netSalary <= 700000)
                {
                    return 25000;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
