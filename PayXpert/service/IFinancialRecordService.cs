using PayXpert.model;

namespace PayXpert.service
{
    public interface IFinancialRecordService
    {
        void AddFinancialRecord(int employeeId, string description, double amount, string recordType);
        FinancialRecord GetFinancialRecordById(int recordId);
        List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId);
        List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate);
    }
}
