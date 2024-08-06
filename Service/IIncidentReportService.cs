using AeMAPI.Models;

namespace AeMAPI.Service
{
    public interface IIncidentReportService
    {
        Task<IEnumerable<Incidentreport>> GetAllReportsAsync();
        Task<Incidentreport> GetReportByIdAsync(int id);
        Task AddReportAsync(Incidentreport report);
    }
}
