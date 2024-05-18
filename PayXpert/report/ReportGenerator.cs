using PayXpert.model;

namespace PayXpert.report
{
    public class ReportGenerator
    {
        // Generate various reports based on data

        // Example method to generate a payroll report
        public void GeneratePayrollReport(List<Payroll> payrolls)
        {
            string reportContent = "Payroll Report:\n\n";
            foreach (var payroll in payrolls)
            {
                reportContent += $"Payroll ID: {payroll.PayrollId}\n";
                reportContent += $"Employee ID: {payroll.EmployeeId}\n";
                reportContent += $"Period Start Date: {payroll.PayPeriodStartDate}\n";
                reportContent += $"Period End Date: {payroll.PayPeriodEndDate}\n";
                reportContent += $"Basic Salary: {payroll.BasicSalary}\n";
                reportContent += $"Overtime Pay: {payroll.OvertimePay}\n";
                reportContent += $"Deductions: {payroll.Deductions}\n";
                reportContent += $"Net Salary: {payroll.NetSalary}\n\n";
            }

            GenerateReport("PayrollReport", reportContent);
        }

        // Example method to generate a tax report
        public void GenerateTaxReport(List<Tax> taxes)
        {
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
        }

        // Example method to generate a financial record report
        public void GenerateFinancialRecordReport(List<FinancialRecord> records)
        {
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
