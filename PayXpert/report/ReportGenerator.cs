using PayXpert.model;
using PayXpert.util;
using System.Data.SqlClient;

namespace PayXpert.report
{
    public class ReportGenerator
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand cmd = null;

        public ReportGenerator()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }
        // Generate various reports based on data

        // Example method to generate a payroll report
        public void GeneratePayrollReport(List<Payroll> payrolls)
        {
            cmd.CommandText = "SELECT * FROM Payroll";
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Payroll payrollData = new Payroll()
                {
                    PayrollId = Convert.ToInt32(reader["PayrollId"]),
                    EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                    PayPeriodStartDate = Convert.ToDateTime(reader["PayPeriodStartDate"]),
                    PayPeriodEndDate = Convert.ToDateTime(reader["PayPeriodEndDate"]),
                    BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                    OvertimePay = Convert.ToDecimal(reader["OvertimePay"]),
                    OvertimeHours = Convert.ToInt32(reader["OvertimeHours"]),
                    DA = Convert.ToDecimal(reader["DA"]),
                    RA = Convert.ToDecimal(reader["RA"]),
                    Deductions = Convert.ToDecimal(reader["Deductions"]),
                    GrossSalary = Convert.ToDecimal(reader["GrossSalary"]),
                    NetSalary = Convert.ToDecimal(reader["NetSalary"])
                };
                payrolls.Add(payrollData);
            }
            string reportContent = "Payroll Report:\n\n";
            foreach (var payroll in payrolls)
            {
                reportContent += $"Payroll ID: {payroll.PayrollId}\n";
                reportContent += $"Employee ID: {payroll.EmployeeId}\n";
                reportContent += $"Period Start Date: {payroll.PayPeriodStartDate}\n";
                reportContent += $"Period End Date: {payroll.PayPeriodEndDate}\n";
                reportContent += $"Basic Salary: {payroll.BasicSalary}\n";
                reportContent += $"Overtime Pay: {payroll.OvertimePay}\n";
                reportContent += $"Overtime Hours: {payroll.OvertimeHours}\n";
                reportContent += $"Dearness Allowance(DA): {payroll.DA}\n";
                reportContent += $"Risk Allowance(RA): {payroll.RA}\n";
                reportContent += $"Deductions: {payroll.Deductions}\n";
                reportContent += $"Gross Salary: {payroll.GrossSalary}\n";
                reportContent += $"Net Salary: {payroll.NetSalary}\n\n";
            }

            GenerateReport("PayrollReport", reportContent);
            sqlConnection.Close();
        }

        // Example method to generate a tax report
        public void GenerateTaxReport(List<Tax> taxes)
        {
            cmd.CommandText = "SELECT * FROM Tax";
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Tax tax = new Tax()
                {
                    TaxId = Convert.ToInt32(reader["TaxId"]),
                    EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                    TaxableIncome = Convert.ToDecimal(reader["TaxableIncome"]),
                    TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
                    TaxYear = Convert.ToInt32(reader["TaxYear"])
                };
                taxes.Add(tax);
            }
            string reportContent = "Tax Report:\n\n";
            foreach (var tax in taxes)
            {
                reportContent += $"Tax ID: {tax.TaxId}\n";
                reportContent += $"Employee ID: {tax.EmployeeId}\n";
                reportContent += $"Tax Year: {tax.TaxYear}\n";
                reportContent += $"Taxable Income: {tax.TaxableIncome}\n";
                reportContent += $"Tax Amount: {tax.TaxAmount}\n\n";
            }

            GenerateReport("TaxReport", reportContent);
            sqlConnection.Close();
        }

        // Example method to generate a financial record report
        public void GenerateFinancialRecordReport(List<FinancialRecord> records)
        {
            cmd.CommandText = "SELECT * FROM FinancialRecord";
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                FinancialRecord financialRecord = new FinancialRecord()
                {
                    RecordId = Convert.ToInt32(reader["RecordId"]),
                    EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                    RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                    RecordType = Convert.ToString(reader["RecordType"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    Description = Convert.ToString(reader["Description"])
                };
                records.Add(financialRecord);
            }
            string reportContent = "Financial Record Report:\n\n";
            foreach (var record in records)
            {
                reportContent += $"Record ID: {record.RecordId}\n";
                reportContent += $"Employee ID: {record.EmployeeId}\n";
                reportContent += $"Record Date: {record.RecordDate}\n";
                reportContent += $"Description: {record.Description}\n";
                reportContent += $"Amount: {record.Amount}\n";
                reportContent += $"Record Type: {record.RecordType}\n\n";
            }

            GenerateReport("FinancialRecordReport", reportContent);
            sqlConnection.Close();
        }

        // Method to generate a generic report
        public void GenerateReport(string reportName, string content)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = $"{reportName}_{timestamp}.txt";
            string path = Path.Combine(Environment.CurrentDirectory, "Reports", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            File.WriteAllText(path, content);

            Console.WriteLine($"Report generated and saved at: {path}");
        }
    }
}
