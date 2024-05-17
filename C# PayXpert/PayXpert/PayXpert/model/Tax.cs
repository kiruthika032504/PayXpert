using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.model
{
    public class Tax
    {
        public int TaxId { get; set; }
        public int EmployeeId { get; set; }
        public int TaxYear { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal TaxAmount { get; set; }

        // Create Constructor
        public Tax()
        {
            TaxId = TaxId;
            EmployeeId = EmployeeId;
            TaxYear = TaxYear;
            TaxableIncome = TaxableIncome;
            TaxAmount = TaxAmount;
        }

        // Overload Constructor with parameter
        public Tax(int taxId, int empId, int taxYear,decimal taxIncome, decimal taxAmount)
        {
            TaxId = taxId;
            EmployeeId = empId;
            TaxYear = taxYear;
            TaxableIncome = taxIncome;
            TaxAmount = taxAmount;
        }
    }
}
