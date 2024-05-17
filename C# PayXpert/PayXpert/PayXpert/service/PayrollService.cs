using PayXpert.exception;
using PayXpert.model;
using PayXpert.util;
using System.Data.SqlClient;

namespace PayXpert.service
{
    public class PayrollService : EmployeeService, IPayrollService
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public PayrollService()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }
        public void GeneratePayroll(int employeeId, decimal basicSalary, DateTime startDate, DateTime endDate)
        {
            try
            {
                sqlConnection.Open();
                // Fetch employee data from the database based on employeeId
                Employee employee = GetEmployeeById(employeeId);
                Payroll payroll = new Payroll();
                if (employeeId <= 0 || startDate >= endDate)
                {
                    throw new InvalidInputException("Invalid input parameters for payroll generation.");
                }
                if (employee == null)
                {
                    // Handle case where employee is not found
                    Console.WriteLine("Employee not found for ID: " + employeeId);
                    return;
                }

                // Calculate salaries, deductions, and net pay (example calculations)

                decimal overtimePay = CalculateOvertimePay(employee, startDate, endDate);
                decimal deductions = CalculateDeductions(employee, basicSalary, startDate, endDate);
                decimal netSalary = CalculateNetSalary(basicSalary, overtimePay, deductions);
                decimal grossSalary = CalculateGrossSalary(basicSalary, overtimePay, deductions);

                // Insert the payroll data into the database
                InsertPayrollData(employeeId, startDate, endDate, basicSalary, overtimePay, deductions,netSalary,grossSalary);  /*netSalary*/

            }
            catch (SqlException ex)
            {                
                Console.WriteLine("Error generating payroll: " + ex.Message);                
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public decimal CalculateGrossSalary(decimal basicSalary, decimal overtimePay, decimal deductions)
        {
            decimal grossSalary = basicSalary + overtimePay;
            return grossSalary;
        }

        public decimal CalculateNetSalary(decimal basicSalary, decimal overtimePay, decimal deductions)
        {
            decimal netSalary = basicSalary + overtimePay - deductions;
            return netSalary;
        }

        private void InsertPayrollData(int employeeId, DateTime startDate, DateTime endDate, decimal basicSalary, decimal overtimePay, decimal deductions, decimal netSalary, decimal grossSalary)
        {
            // Prepare the SQL command to insert payroll data
            cmd.CommandText = @"INSERT INTO Payroll (EmployeeId, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions, NetSalary, GrossSalary)
                       VALUES (@EmployeeId, @StartDate, @EndDate, @BasicSalary, @OvertimePay, @Deductions, @NetSalary, @GrossSalary)";
            // Clear Parameters
            cmd.Parameters.Clear();
            // Add parameters to the command
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@BasicSalary", basicSalary);
            cmd.Parameters.AddWithValue("@OvertimePay", overtimePay);
            cmd.Parameters.AddWithValue("@Deductions", deductions);
            cmd.Parameters.AddWithValue("@NetSalary", netSalary);
            cmd.Parameters.AddWithValue("@GrossSalary", grossSalary);

            // Execute the command
            cmd.ExecuteNonQuery();
        }

        public Payroll GetPayrollById(int payrollId)
        {
            cmd.CommandText = "SELECT * FROM Payroll WHERE PayrollId = @PayrollId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PayrollId", payrollId);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Payroll payroll = new Payroll
                    {
                        PayrollId = Convert.ToInt32(reader["PayrollId"]),
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        PayPeriodStartDate = Convert.ToDateTime(reader["PayPeriodStartDate"]),
                        PayPeriodEndDate = Convert.ToDateTime(reader["PayPeriodEndDate"]),
                        BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                        OvertimePay = Convert.ToDecimal(reader["OvertimePay"]),
                        Deductions = Convert.ToDecimal(reader["Deductions"]),
                        NetSalary = Convert.ToDecimal(reader["NetSalary"]),
                        GrossSalary = Convert.ToDecimal(reader["GrossSalary"])
                    };
                    return payroll;
                }
                else
                {
                    return null; // Payroll not found
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

        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            try
            {
                List<Payroll> payrolls = new List<Payroll>();
                sqlConnection.Open();
                cmd.CommandText = "SELECT * FROM Payroll WHERE EmployeeId = @EmployeeId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Payroll payroll = new Payroll
                    {
                        PayrollId = Convert.ToInt32(reader["PayrollId"]),
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        PayPeriodStartDate = Convert.ToDateTime(reader["PayPeriodStartDate"]),
                        PayPeriodEndDate = Convert.ToDateTime(reader["PayPeriodEndDate"]),
                        BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                        OvertimePay = Convert.ToDecimal(reader["OvertimePay"]),
                        Deductions = Convert.ToDecimal(reader["Deductions"]),
                        NetSalary = Convert.ToDecimal(reader["NetSalary"]),
                        GrossSalary = Convert.ToDecimal(reader["GrossSalary"])
                    };
                    payrolls.Add(payroll);
                }
                return payrolls;
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

        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<Payroll> payrolls = new List<Payroll>();
                sqlConnection.Open();
                cmd.CommandText = "SELECT * FROM Payroll WHERE PayPeriodStartDate >= @StartDate AND PayPeriodEndDate <= @EndDate";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Payroll payroll = new Payroll
                    {
                        PayrollId = Convert.ToInt32(reader["PayrollId"]),
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        PayPeriodStartDate = Convert.ToDateTime(reader["PayPeriodStartDate"]),
                        PayPeriodEndDate = Convert.ToDateTime(reader["PayPeriodEndDate"]),
                        BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                        OvertimePay = Convert.ToDecimal(reader["OvertimePay"]),
                        Deductions = Convert.ToDecimal(reader["Deductions"]),
                        NetSalary = Convert.ToDecimal(reader["NetSalary"]),
                        GrossSalary = Convert.ToDecimal(reader["GrossSalary"])
                    };
                    payrolls.Add(payroll);
                }
                return payrolls;
            }
            catch (SqlException ex)
            {
                
                Console.WriteLine("Error getting payrolls for period: " + ex.Message);
                
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        // Method to calculate overtime pay for an employee
        private decimal CalculateOvertimePay(Employee employee, DateTime startDate, DateTime endDate)
        {
            decimal overtimeRate = 50; // Example overtime rate per hour
            int hoursWorked = GetHoursWorked(employee.EmployeeId, startDate, endDate); // Assume GetHoursWorked method returns hours worked
            decimal overtimePay = overtimeRate * hoursWorked;

            return overtimePay;
        }

        private decimal CalculateDeductions(Employee employee, decimal basicSalary, DateTime startDate, DateTime endDate)
        {
            Payroll payroll = new Payroll();
            decimal deductionRate = 0.08M;
            decimal deductions = basicSalary * deductionRate;
            return deductions;

        }
        // Method to get hours worked by an employee within a specified period
        private int GetHoursWorked(int employeeId, DateTime startDate, DateTime endDate)
        {
            int hoursWorked = 0;
            
            // Prepare the SQL command to fetch hours worked
            cmd.CommandText = "SELECT EmployeeID,DATEDIFF(HOUR, JoiningDate, ISNULL(TerminationDate, GETDATE())) AS TotalHoursWorked FROM Employee " +
                              "WHERE EmployeeId = @EmployeeId AND (JoiningDate <= @EndDate OR TerminationDate IS NULL OR TerminationDate >= @StartDate)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value && result != null)
            {
                hoursWorked = Convert.ToInt32(result);
            }        
            return hoursWorked;
        }
    }
}
