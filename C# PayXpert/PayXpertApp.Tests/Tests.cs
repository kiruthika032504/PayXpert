using NUnit.Framework;
using NUnit.Framework.Internal;
using PayXpert.exception;
using PayXpert.model;
using PayXpert.service;
using static PayXpert.service.TaxService;

namespace PayXpertApp.Tests
{
    public class Tests
    {
        IEmployeeService employeeService = new EmployeeService();
        IFinancialRecordService financialRecordService = new FinancialRecordService();
        IPayrollService payrollService = new PayrollService();
        ITaxService taxService = new TaxService();

        [Test] 
        public void CalculateGrossSalaryForEmployee()
        {
            decimal expectedGrossSalary = 65800;
            decimal actualGrossSalary = payrollService.CalculateGrossSalary(65000, 800, 2000);
            // Assert
            Assert.That(expectedGrossSalary,Is.EqualTo(actualGrossSalary), "Gross salary calculation is incorrect.");
        }
        [Test]
        public void CalculateNetSalaryAfterDeductions()
        {
            decimal expectedNetSalary = 63800;
            decimal actualNetSalary = payrollService.CalculateNetSalary(65000, 800, 2000);
            // Assert
            Assert.That(expectedNetSalary, Is.EqualTo(actualNetSalary), "Net salary calculation is incorrect.");
        }
        [Test]
        public void VerifyTaxCalculationForHighIncomeEmployee()
        {
            decimal expectedTax = 11000;
            decimal actualTax = TaxCalculationHelper.CalculateTaxAmount(55000);
            // Assert
            Assert.That(expectedTax, Is.EqualTo(actualTax), "Net salary calculation is incorrect.");
        }
        [Test]
        public void ProcessPayRollForMultipleEmployees()
        {
            int[] employeeIds = { 1, 2, 3 };
            int[] basicSalary = { 50000, 75000, 90000 };
            DateTime startDate = new DateTime(2024, 5, 1);
            DateTime endDate = new DateTime(2024, 5, 31);

            // Process payroll for each employee in the list
            foreach (var employeeId in employeeIds)
            {
                foreach(var salary in basicSalary)
                {
                    Assert.DoesNotThrow(() => payrollService.GeneratePayroll(employeeId, salary, startDate, endDate));
                }
            }
        }
        [Test]
        public void VerifyErrorHandlingForInvalidEmployeeData_ExceptionThrown()
        {
            // Invalid employee ID and incorrect pay period
            int invalidEmployeeId = 0;
            decimal invalidSalary = 90000;
            DateTime invalidStartDate = new DateTime(2024, 5, 1);
            DateTime invalidEndDate = new DateTime(2024, 4, 30); // End date before start date

            // Verify that an InvalidInputException is thrown
            Assert.Throws<InvalidInputException>(() => payrollService.GeneratePayroll(invalidEmployeeId, invalidSalary, invalidStartDate, invalidEndDate));
        }
    }
}
