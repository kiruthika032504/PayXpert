using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.model
{
    public class FinancialRecord
    {
        public int RecordId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime RecordDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string RecordType { get; set; }

        // Create Constructor
        public FinancialRecord()
        {
            RecordId = RecordId;
            EmployeeId = EmployeeId;
            RecordDate = RecordDate;
            Description = Description;
            Amount = Amount;
            RecordType = RecordType;
        }

        // Overload Constructor with parameters
        public FinancialRecord(int recordId, int empId, DateTime recordDate, string description, decimal amount, string recordType)
        {
            RecordId = recordId;
            EmployeeId = empId;
            RecordDate = recordDate;
            Description = description;
            Amount = amount;
            RecordType = recordType;
        }
    }
}
