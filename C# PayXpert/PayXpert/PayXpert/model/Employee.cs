using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime TerminationDate { get; set; }

        // Create Constructor
        public Employee()
        {
            EmployeeId = EmployeeId;
            FirstName = FirstName;
            LastName = LastName;
            DateOfBirth = DateOfBirth;
            Gender = Gender;
            Email = Email;
            PhoneNumber = PhoneNumber;
            Address = Address;
            Position = Position;
            JoiningDate = JoiningDate;
            TerminationDate = TerminationDate;
        }

        // Overload Constructor with parameters

        public Employee(int empId, string fName, string lName, DateTime dob, string gender, string email, string phoneNo, string address, string position, DateTime joiningDate, DateTime terminationDate)
        {
            EmployeeId = empId;
            FirstName = fName;
            LastName = lName;
            DateOfBirth = dob;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNo;
            Address = address;
            Position = position;
            JoiningDate = joiningDate;
            TerminationDate = terminationDate;
        }

        // CalculateAge Method
        public int CalculateAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if(DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
       

    }
}
