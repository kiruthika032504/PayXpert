using PayXpert.exception;
using PayXpert.model;
using PayXpert.report;
using PayXpert.service;
using PayXpert.validation;

namespace PayXpert.app
{
    public class PayXpertApp
    {
        static readonly IEmployeeService _employeeService = new EmployeeService();
        static readonly IPayrollService _payrollService = new PayrollService();
        static readonly ITaxService _taxService = new TaxService();
        static readonly IFinancialRecordService _financialRecordService = new FinancialRecordService();
        static readonly IUserService _iuserService = new UserService();

        public void Run()
        {
            // Perform login or register
            User user = new User();
            bool loggedIn = false;

            while (!loggedIn)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        RegisterUser(_iuserService);
                        break;
                    case 2:
                        loggedIn = LoginUser(_iuserService);
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Exiting...");
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. Please choose again.");
                        Console.ResetColor();
                        break;
                }
            }

            while (loggedIn)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Employee Operations");
                Console.WriteLine("2. Payroll Operations");
                Console.WriteLine("3. Tax Operations");
                Console.WriteLine("4. Financial Record Operations");
                Console.WriteLine("5. Generate Reports");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your Choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        EmployeeOperationsMenu();
                        break;
                    case 2:
                        PayrollOperationsMenu();
                        break;
                    case 3:
                        TaxOperationsMenu();
                        break;
                    case 4:
                        FinancialRecordOperationsMenu();
                        break;
                    case 5:
                        GenerateReportsMenu();
                        break;
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Exiting program...");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine();
            }
        }
        static void RegisterUser(IUserService userService)
        {
            Console.Write("Enter username to register: ");
            string newUsername = Console.ReadLine();
            Console.Write("Enter password to register: ");
            string newPassword = Console.ReadLine();

            if (userService.IsUsernameExists(newUsername))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Username already exists. Please choose a different one.");
                Console.ResetColor();
            }
            else
            {
                userService.RegisterUser(newUsername, newPassword);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("User registered successfully!");
                Console.ResetColor();
            }
        }

        static bool LoginUser(IUserService userService)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (userService.IsLoginValid(username, password))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Login successful!");
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(new InvalidInputException("Invalid username or password. Please try again."));
                Console.ResetColor();
                return false;
            }
        }
        private void EmployeeOperationsMenu()
        {
            while (true)
            {
                Console.WriteLine("Employee Operations Menu:");
                Console.WriteLine("1. Get Employee by ID");
                Console.WriteLine("2. Get All Employees");
                Console.WriteLine("3. Add Employee");
                Console.WriteLine("4. Update Employee");
                Console.WriteLine("5. Remove Employee");
                Console.WriteLine("0. Back to Main Menu");

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
                    case 0:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine();
            }
        }

        private void PayrollOperationsMenu()
        {
            while (true)
            {
                Console.WriteLine("Payroll Operations Menu:");
                Console.WriteLine("6. Generate Payroll");
                Console.WriteLine("7. Get Payroll by ID");
                Console.WriteLine("8. Get Payrolls for Employee");
                Console.WriteLine("9. Get Payrolls for Period");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("Enter your Choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
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
                    case 0:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine();
            }
        }

        private void TaxOperationsMenu()
        {
            while (true)
            {
                Console.WriteLine("Tax Operations Menu:");
                Console.WriteLine("10. Calculate Tax");
                Console.WriteLine("11. Get Tax by ID");
                Console.WriteLine("12. Get Taxes for Employee");
                Console.WriteLine("13. Get Taxes for Year");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("Enter your Choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
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
                    case 0:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine();
            }
        }

        private void FinancialRecordOperationsMenu()
        {
            while (true)
            {
                Console.WriteLine("Financial Record Operations Menu:");
                Console.WriteLine("14. Add Financial Record");
                Console.WriteLine("15. Get Financial Record by ID");
                Console.WriteLine("16. Get Financial Records for Employee");
                Console.WriteLine("17. Get Financial Records for Date");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("Enter your Choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
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
                    case 0:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine();
            }
        }

        private void GenerateReportsMenu()
        {
            List<Payroll> payrolls = new List<Payroll>();
            var taxes = new List<Tax>();
            var financialRecords = new List<FinancialRecord>();
            var reportGenerator = new ReportGenerator();
            while (true)
            {
                Console.WriteLine("Generate Reports Menu:");
                Console.WriteLine("18. Generate Payroll Report");
                Console.WriteLine("19. Generate Tax Report");
                Console.WriteLine("20. Generate Financial Record Report");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("Enter your Choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
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
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee not found.");
                    Console.ResetColor();
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No employees found.");
                    Console.ResetColor();
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();   
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
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
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Employee added successfully.");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Joining Date must be before the current date.");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Joining Date format.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Phone Number format.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Email format.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Date of Birth format.");
                    Console.ResetColor();
                }
            }
            catch (InvalidInputException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
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
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Employee updated successfully.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Joining Date format.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Date of Birth format.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee not found.");
                    Console.ResetColor();
                }
            }
            catch (InvalidInputException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Employee removed successfully.");
                Console.ResetColor();
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        private static void GeneratePayroll()
        {
            try
            {
                Console.WriteLine("Enter Employee ID:");
                int employeeId = int.Parse(Console.ReadLine());
                var existingEmployee = _employeeService.GetEmployeeById(employeeId);
                if (existingEmployee != null)
                {
                    Console.WriteLine($"\nExisting Employee Details:");
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
                }
                startDate:  DateTime startDate = GetDateInput("\nEnter Pay Period Start Date (YYYY-MM-DD):");
                endDate: DateTime endDate = GetDateInput("Enter Pay Period End Date (YYYY-MM-DD):");
                // Validate the date range
                if(startDate > endDate)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(new InvalidInputException("Start Date must not exceed End Date"));
                    Console.ResetColor();
                }
                else if(startDate > DateTime.Now)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(new InvalidDataException("Pay Period Start and End Date must not exceed Today's date"));
                    Console.ResetColor();
                    goto startDate;
                }
                else if(endDate > DateTime.Now)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(new InvalidDataException("Pay Period Start and End Date must not exceed Today's date"));
                    Console.ResetColor();
                    goto endDate;
                }
                else if ((endDate - startDate).Days > 31)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(new InvalidInputException("The pay period duration should not exceed one month."));
                    Console.ResetColor();
                }
                Console.WriteLine("Enter Basic Salary:");
                decimal basicSalary = decimal.Parse(Console.ReadLine());
                da: Console.WriteLine("Enter DA (Dearness Allowance):");
                decimal DA = decimal.Parse(Console.ReadLine());
                ra:  Console.WriteLine("Enter RA (Risk Allowance):");
                decimal RA = decimal.Parse(Console.ReadLine());
                overtimeHrs:  Console.WriteLine("Enter Overtime Hours:");
                decimal overtimeHrs = decimal.Parse(Console.ReadLine());
                deductions:  Console.WriteLine("Enter Deductions:");
                decimal deductions = decimal.Parse(Console.ReadLine());
                if (DA < 0 || RA < 0 || deductions < 0 || overtimeHrs < 0)
                {
                    Console.WriteLine(new InvalidDataException("Invalid Data Input"));
                    if (DA < 0) goto da;
                    else if (RA < 0) goto ra;
                    else if (deductions < 0) goto deductions;
                    else if (overtimeHrs < 0) goto overtimeHrs;
                }
                // Call _payrollService.GeneratePayroll(employeeId, startDate, endDate)
                _payrollService.GeneratePayroll(employeeId, basicSalary, deductions, RA, DA, overtimeHrs, startDate, endDate);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Payroll generated successfully.");
                Console.ResetColor();
            }
            catch (PayrollGenerationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Payroll not found.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No payrolls found for the employee.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No payrolls found for the specified period.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Invalid date format. Please enter a valid date (YYYY-MM-DD).");
                    Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tax calculated successfully.");
                Console.ResetColor();
            }
            catch (TaxCalculationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tax not found.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No taxes found for the employee.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No taxes found for the specified year.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Financial record added successfully.");
                Console.ResetColor();
            }
            catch (FinancialRecordException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Financial record not found.");
                    Console.ResetColor();
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No financial records found for the employee.");
                    Console.ResetColor();
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No financial records found for the specified date.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
