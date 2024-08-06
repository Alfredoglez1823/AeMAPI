using AeMAPI.Models;

namespace AeMAPI.Repository
{
    public interface IIncidentReportRepository
    {
        Task<IEnumerable<Incidentreport>> GetAllReportsAsync();
        Task<Incidentreport> GetReportByIdAsync(int id);
        Task AddReportAsync(Incidentreport report);
    }
}
