using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.validation
{
    public class ValidationService
    {
        // Input validation methods

        // Validate email format
        public bool ValidateEmail(string email)
        {
            // Example validation: check if email contains "@" symbol
            return email.Contains("@");
        }

        // Validate phone number format
        public bool ValidatePhoneNumber(string phoneNumber)
        {
            // Example validation: check if phone number contains only digits and has a valid length
            return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 10 && phoneNumber.Length <= 15;
        }

        // Business rule enforcement methods

        // Check if an employee's joining date is before termination date
        public bool IsJoiningDateBeforeTerminationDate(DateTime joiningDate, DateTime terminationDate)
        {
            return joiningDate < terminationDate;
        }

        // Check if basic salary is greater than zero
        public bool IsBasicSalaryValid(double basicSalary)
        {
            return basicSalary > 0;
        }

    }
}
