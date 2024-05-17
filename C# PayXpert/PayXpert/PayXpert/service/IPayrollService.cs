using PayXpert.model;

namespace PayXpert.service
{
    public interface IPayrollService
    {
        void GeneratePayroll(int employeeId, decimal basicSalary, DateTime startDate, DateTime endDate);
        Payroll GetPayrollById(int payrollId);
        List<Payroll> GetPayrollsForEmployee(int employeeId);
        List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
        decimal CalculateGrossSalary(decimal basicSalary, decimal overtimePay, decimal deductions);
        decimal CalculateNetSalary(decimal basicSalary, decimal overtimePay, decimal deductions);
    }
}
