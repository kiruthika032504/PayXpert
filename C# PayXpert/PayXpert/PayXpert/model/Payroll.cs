using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.model
{
    public class Payroll
    {
        public int PayrollId { get; set; }
        public int EmployeeId {  get; set; }
        public DateTime PayPeriodStartDate { get; set; }
        public DateTime PayPeriodEndDate { get; set;}
        public decimal BasicSalary { get; set; }
        public decimal OvertimePay {  get; set; }
        public decimal Deductions {  get; set; }
        public decimal GrossSalary {  get; set; }
        public decimal NetSalary {  get; set; }
        
        // Create Constructor
        public Payroll()
        {
            PayrollId = PayrollId;
            EmployeeId = EmployeeId;
            PayPeriodStartDate = PayPeriodStartDate;
            PayPeriodEndDate = PayPeriodEndDate;
            BasicSalary = BasicSalary;
            OvertimePay = OvertimePay;
            Deductions = Deductions;
            GrossSalary = GrossSalary;
            NetSalary = NetSalary;
        }

        // Overload Constructor
        public Payroll(int payId, int empId, DateTime payStartDate, DateTime payEndDate,decimal basicSalary, decimal overtimeDue, decimal deductions, decimal grossSalary, decimal netSalary)
        {
            PayrollId = payId;
            EmployeeId = empId;
            PayPeriodStartDate = payStartDate;
            PayPeriodEndDate = payEndDate;
            BasicSalary = basicSalary;
            OvertimePay = overtimeDue;
            Deductions = deductions;
            GrossSalary = grossSalary;
            NetSalary = netSalary;
        }

    }
}
