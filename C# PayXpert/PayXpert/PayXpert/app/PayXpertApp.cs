using PayXpert.exception;
using PayXpert.model;
using PayXpert.service;
using PayXpert.validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.app
{
    public class PayXpertApp
    {
        static readonly IEmployeeService _employeeService = new EmployeeService();
        static readonly IPayrollService _payrollService = new PayrollService();
        static readonly ITaxService _taxService = new TaxService();
        static readonly IFinancialRecordService _financialRecordService = new FinancialRecordService();

        public void Run()
        {
            var payrolls = new List<Payroll>();
            var taxes = new List<Tax>();
            var financialRecords = new List<FinancialRecord>();

            var reportGenerator = new report.ReportGenerator();
            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Get Employee by ID");
                Console.WriteLine("2. Get All Employees");
                Console.WriteLine("3. Add Employee");
                Console.WriteLine("4. Update Employee");
                Console.WriteLine("5. Remove Employee");
                Console.WriteLine("6. Generate Payroll");
                Console.WriteLine("7. Get Payroll by ID");
                Console.WriteLine("8. Get Payrolls for Employee");
                Console.WriteLine("9. Get Payrolls for Period");
                Console.WriteLine("10. Calculate Tax");
                Console.WriteLine("11. Get Tax by ID");
                Console.WriteLine("12. Get Taxes for Employee");
                Console.WriteLine("13. Get Taxes for Year");
                Console.WriteLine("14. Add Financial Record");
                Console.WriteLine("15. Get Financial Record by ID");
                Console.WriteLine("16. Get Financial Records for Employee");
                Console.WriteLine("17. Get Financial Records for Date");
                Console.WriteLine("18. Generate PayRoll Report");
                Console.WriteLine("19. Generate Tax Report");
                Console.WriteLine("20. Generate Financial Record Report");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your Choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        GetEmployeeById();
                        break;
                    case 2:
                        GetAllEmployees();
                        break;
                    case 3:
                        AddEmployee();
                        break;
                    case 4:
                        UpdateEmployee();
                        break;
                    case 5:
                        RemoveEmployee();
                        break;
                    case 6:
                        GeneratePayroll();
                        break;
                    case 7:
                        GetPayrollById();
                        break;
                    case 8:
                        GetPayrollsForEmployee();
                        break;
                    case 9:
                        GetPayrollsForPeriod();
                        break;
                    case 10:
                        CalculateTax();
                        break;
                    case 11:
                        GetTaxById();
                        break;
                    case 12:
                        GetTaxesForEmployee();
                        break;
                    case 13:
                        GetTaxesForYear();
                        break;
                    case 14:
                        AddFinancialRecord();
                        break;
                    case 15:
                        GetFinancialRecordById();
                        break;
                    case 16:
                        GetFinancialRecordsForEmployee();
                        break;
                    case 17:
                        GetFinancialRecordsForDate();
                        break;
                    case 18:
                        reportGenerator.GeneratePayrollReport(payrolls);
                        break;
                    case 19:
                        reportGenerator.GenerateTaxReport(taxes);
                        break;
                    case 20:
                        reportGenerator.GenerateFinancialRecordReport(financialRecords);
                        break;
                    case 0:
                        Console.WriteLine("Exiting program...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        
        // Implement methods for each menu option
        private static void GetEmployeeById()
        {
            Console.WriteLine("Enter Employee ID:");
            int employeeId = int.Parse(Console.ReadLine());

            try
            {
                var employee = _employeeService.GetEmployeeById(employeeId);
                if (employee != null)
                {
                    Console.WriteLine($"Employee ID: {employee.EmployeeId}");
                    Console.WriteLine($"Name: {employee.FirstName} {employee.LastName}");
                    Console.WriteLine($"Date of Birth: {employee.DateOfBirth}");
                    Console.WriteLine($"Gender: {employee.Gender}");
                    Console.WriteLine($"Email: {employee.Email}");
                    Console.WriteLine($"Phone Number: {employee.PhoneNumber}");
                    Console.WriteLine($"Address: {employee.Address}");
                    Console.WriteLine($"Position: {employee.Position}");
                    Console.WriteLine($"Joining Date: {employee.JoiningDate}");
                    Console.WriteLine($"Termination Date: {employee.TerminationDate}");
                    Console.WriteLine($"Age: {employee.CalculateAge()}");
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetAllEmployees()
        {
            try
            {
                List<Employee> employees = _employeeService.GetAllEmployees();
                if (employees != null && employees.Count > 0)
                {
                    Console.WriteLine("List of Employees:");
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Employee ID: {employee.EmployeeId}");
                        Console.WriteLine($"Name: {employee.FirstName} {employee.LastName}");
                        Console.WriteLine($"Date of Birth: {employee.DateOfBirth}");
                        Console.WriteLine($"Gender: {employee.Gender}");
                        Console.WriteLine($"Email: {employee.Email}");
                        Console.WriteLine($"Phone Number: {employee.PhoneNumber}");
                        Console.WriteLine($"Address: {employee.Address}");
                        Console.WriteLine($"Position: {employee.Position}");
                        Console.WriteLine($"Joining Date: {employee.JoiningDate}");
                        Console.WriteLine($"Termination Date: {employee.TerminationDate}");
                        Console.WriteLine($"Age: {employee.CalculateAge()}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No employees found.");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void AddEmployee()
        {
            try
            {
                ValidationService validator = new ValidationService();

                Console.WriteLine("Enter employee details:");
                Console.Write("First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Last Name: ");
                string lastName = Console.ReadLine();

                Console.Write("Date of Birth (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                {
                    Console.Write("Gender: ");
                    string gender = Console.ReadLine();

                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    if (validator.ValidateEmail(email))
                    {
                        Console.Write("Phone Number: ");
                        string phoneNumber = Console.ReadLine();
                        if (validator.ValidatePhoneNumber(phoneNumber))
                        {
                            Console.Write("Address: ");
                            string address = Console.ReadLine();

                            Console.Write("Position: ");
                            string position = Console.ReadLine();

                            Console.Write("Joining Date (YYYY-MM-DD): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime joiningDate))
                            {
                                if (validator.IsJoiningDateBeforeTerminationDate(joiningDate, DateTime.Now))
                                {
                                    Employee employee = new Employee
                                    {
                                        FirstName = firstName,
                                        LastName = lastName,
                                        DateOfBirth = dateOfBirth,
                                        Gender = gender,
                                        Email = email,
                                        PhoneNumber = phoneNumber,
                                        Address = address,
                                        Position = position,
                                        JoiningDate = joiningDate,
                                        TerminationDate = DateTime.MinValue
                                    };


                                    // Call _employeeService.AddEmployee(employeeData)
                                    _employeeService.AddEmployee(employee);
                                    Console.WriteLine("Employee added successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Joining Date must be before the current date.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Joining Date format.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Phone Number format.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Email format.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Date of Birth format.");
                }
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private static void UpdateEmployee()
        {
            try
            {
                Console.WriteLine("Enter Employee ID:");
                int employeeId = int.Parse(Console.ReadLine());
                var existingEmployee = _employeeService.GetEmployeeById(employeeId);
                if (existingEmployee != null)
                {
                    Console.WriteLine($"Existing Employee Details:");
                    Console.WriteLine($"First Name: {existingEmployee.FirstName}");
                    Console.WriteLine($"Last Name: {existingEmployee.LastName}");
                    Console.WriteLine($"Date of Birth: {existingEmployee.DateOfBirth}");
                    Console.WriteLine($"Gender: {existingEmployee.Gender}");
                    Console.WriteLine($"Email: {existingEmployee.Email}");
                    Console.WriteLine($"Phone Number: {existingEmployee.PhoneNumber}");
                    Console.WriteLine($"Address: {existingEmployee.Address}");
                    Console.WriteLine($"Position: {existingEmployee.Position}");
                    Console.WriteLine($"Joining Date: {existingEmployee.JoiningDate}");
                    Console.WriteLine($"Termination Date: {existingEmployee.TerminationDate}");

                    Console.WriteLine("Enter updated employee details:");
                    Console.Write("First Name: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Last Name: ");
                    string lastName = Console.ReadLine();

                    Console.Write("Date of Birth (YYYY-MM-DD): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                    {
                        Console.Write("Gender: ");
                        string gender = Console.ReadLine();

                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Phone Number: ");
                        string phoneNumber = Console.ReadLine();

                        Console.Write("Address: ");
                        string address = Console.ReadLine();

                        Console.Write("Position: ");
                        string position = Console.ReadLine();

                        Console.Write("Joining Date (YYYY-MM-DD): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime joiningDate))
                        {
                            Console.Write("Termination Date (YYYY-MM-DD): ");
                            DateTime terminationDate = DateTime.Parse(Console.ReadLine());
                            if (employeeId == existingEmployee.EmployeeId)
                            {
                                Employee updatedEmployee = new Employee
                                {
                                    FirstName = firstName,
                                    LastName = lastName,
                                    DateOfBirth = dateOfBirth,
                                    Gender = gender,
                                    Email = email,
                                    PhoneNumber = phoneNumber,
                                    Address = address,
                                    Position = position,
                                    JoiningDate = joiningDate,
                                    TerminationDate = terminationDate
                                };
                                // Call _employeeService.UpdateEmployee(updatedEmployee)
                                _employeeService.UpdateEmployee(updatedEmployee);
                            }
                            Console.WriteLine("Employee updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Joining Date format.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Date of Birth format.");
                    }
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void RemoveEmployee()
        {
            try
            {
                Console.WriteLine("Enter Employee ID:");
                int employeeId = int.Parse(Console.ReadLine());
                // Call _employeeService.RemoveEmployee(employeeId)
                _employeeService.RemoveEmployee(employeeId);
                Console.WriteLine("Employee removed successfully.");
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GeneratePayroll()
        {
            try
            {
                Console.WriteLine("Enter Employee ID:");
                int employeeId = int.Parse(Console.ReadLine());
                DateTime startDate = GetDateInput("Enter Pay Period Start Date (YYYY-MM-DD):");
                DateTime endDate = GetDateInput("Enter Pay Period End Date (YYYY-MM-DD):");
                Console.WriteLine("Enter Basic Salary:");
                decimal basicSalary = decimal.Parse(Console.ReadLine());
                // Call _payrollService.GeneratePayroll(employeeId, startDate, endDate)
                _payrollService.GeneratePayroll(employeeId, basicSalary, startDate, endDate);
                Console.WriteLine("Payroll generated successfully.");
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetPayrollById()
        {
            try
            {

                Console.WriteLine("Enter Payroll ID:");
                int payrollId = int.Parse(Console.ReadLine());
                var payroll = _payrollService.GetPayrollById(payrollId);
                if (payroll != null)
                {
                    Console.WriteLine($"Payroll ID: {payroll.PayrollId}");
                    Console.WriteLine($"Employee ID: {payroll.EmployeeId}");
                    Console.WriteLine($"Pay Period Start Date: {payroll.PayPeriodStartDate}");
                    Console.WriteLine($"Pay Period End Date: {payroll.PayPeriodEndDate}");
                    Console.WriteLine($"Basic Salary: {payroll.BasicSalary}");
                    Console.WriteLine($"Overtime Pay: {payroll.OvertimePay}");
                    Console.WriteLine($"Deductions: {payroll.Deductions}");
                    Console.WriteLine($"Net Salary: {payroll.NetSalary}");
                    Console.WriteLine($"Gross Salary: {payroll.GrossSalary}");
                }
                else
                {
                    Console.WriteLine("Payroll not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetPayrollsForEmployee()
        {
            try
            {
                Console.WriteLine("Enter Employee ID:");
                int employeeId = int.Parse(Console.ReadLine());
                var payrolls = _payrollService.GetPayrollsForEmployee(employeeId);
                if (payrolls != null && payrolls.Count > 0)
                {
                    Console.WriteLine($"Payrolls for Employee ID {employeeId}:");
                    foreach (var payroll in payrolls)
                    {
                        Console.WriteLine($"Payroll ID: {payroll.PayrollId}");
                        Console.WriteLine($"Employee ID: {payroll.EmployeeId}");
                        Console.WriteLine($"Pay Period Start Date: {payroll.PayPeriodStartDate}");
                        Console.WriteLine($"Pay Period End Date: {payroll.PayPeriodEndDate}");
                        Console.WriteLine($"Basic Salary: {payroll.BasicSalary}");
                        Console.WriteLine($"Overtime Pay: {payroll.OvertimePay}");
                        Console.WriteLine($"Deductions: {payroll.Deductions}");
                        Console.WriteLine($"Net Salary: {payroll.NetSalary}");
                        Console.WriteLine($"Gross Salary: {payroll.GrossSalary}");
                    }
                }
                else
                {
                    Console.WriteLine("No payrolls found for the employee.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetPayrollsForPeriod()
        {
            try
            {
                DateTime startDate = GetDateInput("Enter Pay Period Start Date (YYYY-MM-DD):");
                DateTime endDate = GetDateInput("Enter Pay Period End Date (YYYY-MM-DD):");

                var payrolls = _payrollService.GetPayrollsForPeriod(startDate, endDate);
                if (payrolls != null && payrolls.Count > 0)
                {
                    Console.WriteLine($"Payrolls for Period {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}:");
                    foreach (var payroll in payrolls)
                    {
                        Console.WriteLine($"Payroll ID: {payroll.PayrollId}");
                        Console.WriteLine($"Employee ID: {payroll.EmployeeId}");
                        Console.WriteLine($"Pay Period Start Date: {payroll.PayPeriodStartDate}");
                        Console.WriteLine($"Pay Period End Date: {payroll.PayPeriodEndDate}");
                        Console.WriteLine($"Basic Salary: {payroll.BasicSalary}");
                        Console.WriteLine($"Overtime Pay: {payroll.OvertimePay}");
                        Console.WriteLine($"Deductions: {payroll.Deductions}");
                        Console.WriteLine($"Net Salary: {payroll.NetSalary}");
                        Console.WriteLine($"Gross Salary: {payroll.GrossSalary}");
                    }
                }
                else
                {
                    Console.WriteLine("No payrolls found for the specified period.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Helper method to get date input from the user
        private static DateTime GetDateInput(string prompt)
        {
            Console.Write(prompt + " ");
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out DateTime inputDate))
                {
                    return inputDate;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date (YYYY-MM-DD).");
                }
            }
        }

        private static void CalculateTax()
        {
            try
            {
                Console.WriteLine("Enter Employee ID:");
                int employeeId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Tax Year:");
                int taxYear = int.Parse(Console.ReadLine());

                // Call _taxService.CalculateTax(employeeId, taxYear)
                _taxService.CalculateTax(employeeId, taxYear);
                Console.WriteLine("Tax calculated successfully.");
            }
            catch (TaxCalculationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetTaxById()
        {
            try
            {

                Console.WriteLine("Enter Tax ID:");
                int taxId = int.Parse(Console.ReadLine());

                var tax = _taxService.GetTaxById(taxId);
                if (tax != null)
                {
                    Console.WriteLine($"Tax ID: {tax.TaxId}");
                    Console.WriteLine($"Employee ID: {tax.EmployeeId}");
                    Console.WriteLine($"Tax Year: {tax.TaxYear}");
                    Console.WriteLine($"Taxable Income: {tax.TaxableIncome}");
                    Console.WriteLine($"Tax Amount: {tax.TaxAmount}");
                }
                else
                {
                    Console.WriteLine("Tax not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetTaxesForEmployee()
        {
            try
            {

                Console.WriteLine("Enter Employee ID:");
                int employeeId = int.Parse(Console.ReadLine());

                var taxes = _taxService.GetTaxesForEmployee(employeeId);
                if (taxes != null && taxes.Count > 0)
                {
                    Console.WriteLine($"Taxes for Employee ID {employeeId}:");
                    foreach (var tax in taxes)
                    {
                        Console.WriteLine($"Tax ID: {tax.TaxId}");
                        Console.WriteLine($"Tax Year: {tax.TaxYear}");
                        Console.WriteLine($"Taxable Income: {tax.TaxableIncome}");
                        Console.WriteLine($"Tax Amount: {tax.TaxAmount}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No taxes found for the employee.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetTaxesForYear()
        {
            try
            {
                Console.WriteLine("Enter Tax Year:");
                int taxYear = int.Parse(Console.ReadLine());

                var taxes = _taxService.GetTaxesForYear(taxYear);
                if (taxes != null && taxes.Count > 0)
                {
                    Console.WriteLine($"Taxes for Tax Year {taxYear}:");
                    foreach (var tax in taxes)
                    {
                        Console.WriteLine($"Tax ID: {tax.TaxId}");
                        Console.WriteLine($"Employee ID: {tax.EmployeeId}");
                        Console.WriteLine($"Taxable Income: {tax.TaxableIncome}");
                        Console.WriteLine($"Tax Amount: {tax.TaxAmount}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No taxes found for the specified year.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void AddFinancialRecord()
        {
            try
            {
                Console.WriteLine("Enter Employee ID:");
                int employeeId = int.Parse(Console.ReadLine());
                Console.Write("Enter Description: ");
                string description = Console.ReadLine();

                Console.WriteLine("Enter Amount:");
                double amount = double.Parse(Console.ReadLine());

                Console.Write("Enter Record Type: ");
                string recordType = Console.ReadLine();

                // Call _financialRecordService.AddFinancialRecord(employeeId, description, amount, recordType)
                _financialRecordService.AddFinancialRecord(employeeId, description, amount, recordType);
                Console.WriteLine("Financial record added successfully.");
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetFinancialRecordById()
        {
            try
            {
                Console.WriteLine("Enter Financial Record ID:");
                int recordId = int.Parse(Console.ReadLine());

                var record = _financialRecordService.GetFinancialRecordById(recordId);
                if (record != null)
                {
                    Console.WriteLine($"Record ID: {record.RecordId}");
                    Console.WriteLine($"Employee ID: {record.EmployeeId}");
                    Console.WriteLine($"Record Date: {record.RecordDate}");
                    Console.WriteLine($"Description: {record.Description}");
                    Console.WriteLine($"Amount: {record.Amount}");
                    Console.WriteLine($"Record Type: {record.RecordType}");
                }
                else
                {
                    Console.WriteLine("Financial record not found.");
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetFinancialRecordsForEmployee()
        {
            try
            {
                Console.WriteLine("Enter Employee ID:");
                int employeeId = int.Parse(Console.ReadLine());

                var records = _financialRecordService.GetFinancialRecordsForEmployee(employeeId);
                if (records != null && records.Count > 0)
                {
                    Console.WriteLine($"Financial Records for Employee ID {employeeId}:");
                    foreach (var record in records)
                    {
                        Console.WriteLine($"Record ID: {record.RecordId}");
                        Console.WriteLine($"Record Date: {record.RecordDate}");
                        Console.WriteLine($"Description: {record.Description}");
                        Console.WriteLine($"Amount: {record.Amount}");
                        Console.WriteLine($"Record Type: {record.RecordType}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No financial records found for the employee.");
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetFinancialRecordsForDate()
        {
            try
            {
                DateTime recordDate = GetDateInput("Enter Record Date (YYYY-MM-DD):");
                var records = _financialRecordService.GetFinancialRecordsForDate(recordDate);
                if (records != null && records.Count > 0)
                {
                    Console.WriteLine($"Financial Records for Date {recordDate:yyyy-MM-dd}:");
                    foreach (var record in records)
                    {
                        Console.WriteLine($"Record ID: {record.RecordId}");
                        Console.WriteLine($"Employee ID: {record.EmployeeId}");
                        Console.WriteLine($"Description: {record.Description}");
                        Console.WriteLine($"Amount: {record.Amount}");
                        Console.WriteLine($"Record Type: {record.RecordType}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No financial records found for the specified date.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
