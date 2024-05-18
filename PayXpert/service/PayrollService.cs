using PayXpert.exception;
using PayXpert.model;
using PayXpert.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PayXpert.service
{
    public class PayrollService : EmployeeService, IPayrollService
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand cmd = null;

        public PayrollService()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }

        public void GeneratePayroll(int employeeId, decimal basicSalary, decimal deductions, decimal DA, decimal RA, decimal overtimeHrs, DateTime startDate, DateTime endDate)
        {
            try
            {
                sqlConnection.Open();

                // Fetch employee data from the database based on employeeId
                Employee employee = GetEmployeeById(employeeId);
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
                decimal overtimePay = CalculateOvertimePay(employeeId, basicSalary, DA, RA, overtimeHrs, startDate, endDate);
                decimal netSalary = CalculateNetSalary(basicSalary, overtimePay, deductions);
                decimal grossSalary = CalculateGrossSalary(basicSalary, overtimePay, deductions);

                // Insert the payroll data into the database
                InsertPayrollData(employeeId, startDate, endDate, basicSalary, DA, RA, (int)overtimeHrs, overtimePay, deductions, netSalary, grossSalary);
            }
            catch(PayrollGenerationException ex)
            {
                Console.WriteLine($"Error generating Payroll : {ex.Message}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error generating payroll: " + ex.Message);
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public decimal CalculateGrossSalary(decimal basicSalary, decimal overtimePay, decimal deductions)
        {
            return basicSalary + overtimePay;
        }

        public decimal CalculateNetSalary(decimal basicSalary, decimal overtimePay, decimal deductions)
        {
            return basicSalary + overtimePay - deductions;
        }

        private void InsertPayrollData(int employeeId, DateTime startDate, DateTime endDate, decimal basicSalary, decimal DA, decimal RA, int overtimeHrs, decimal overtimePay, decimal deductions, decimal netSalary, decimal grossSalary)
        {
            cmd.CommandText = @"INSERT INTO Payroll (EmployeeId, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, DA, RA, OvertimeHours, OvertimePay, Deductions, NetSalary, GrossSalary)
                                VALUES (@EmployeeId, @StartDate, @EndDate, @BasicSalary, @DA, @RA, @OvertimeHours, @OvertimePay, @Deductions, @NetSalary, @GrossSalary)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@BasicSalary", basicSalary);
            cmd.Parameters.AddWithValue("@DA", DA);
            cmd.Parameters.AddWithValue("@RA", RA);
            cmd.Parameters.AddWithValue("@OvertimeHours", overtimeHrs);
            cmd.Parameters.AddWithValue("@OvertimePay", overtimePay);
            cmd.Parameters.AddWithValue("@Deductions", deductions);
            cmd.Parameters.AddWithValue("@NetSalary", netSalary);
            cmd.Parameters.AddWithValue("@GrossSalary", grossSalary);

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
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Payroll
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
                    }
                }
                return null;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            List<Payroll> payrolls = new List<Payroll>();
            cmd.CommandText = "SELECT * FROM Payroll WHERE EmployeeId = @EmployeeId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
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
                }
                return payrolls;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error getting payrolls for employee: " + ex.Message);
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrolls = new List<Payroll>();
            cmd.CommandText = "SELECT * FROM Payroll WHERE PayPeriodStartDate >= @StartDate AND PayPeriodEndDate <= @EndDate";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
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

        public decimal CalculateOvertimePay(int employeeId, decimal basicSalary, decimal DA, decimal RA, decimal overtimeHrs, DateTime startDate, DateTime endDate)
        {
            cmd.CommandText = "SELECT (DAY(@endDate) - DAY(@startDate)) AS TotalDays FROM Payroll WHERE EmployeeID = @EmployeeId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);

            decimal TotalDays = 0;
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        TotalDays = Convert.ToDecimal(reader["TotalDays"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            
            decimal overTimeValue = (basicSalary + DA + RA) / TotalDays;
            decimal houlyOvertimePay = 2 * overTimeValue;
            decimal overtimePay = houlyOvertimePay * overtimeHrs;
            return Math.Round(overtimePay, 2);
        }
    }
}
