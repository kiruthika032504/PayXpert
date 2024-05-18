using PayXpert.model;

namespace PayXpert.service
{
    public interface ITaxService
    {
        void CalculateTax(int employeeId, int taxYear);
        Tax GetTaxById(int taxId);
        List<Tax> GetTaxesForEmployee(int employeeId);
        List<Tax> GetTaxesForYear(int taxYear);
    }
}
