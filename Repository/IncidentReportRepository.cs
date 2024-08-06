using AeMAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AeMAPI.Repository
{
    public class IncidentReportRepository : IIncidentReportRepository
    {
        private readonly PostgresContext _context;

        public IncidentReportRepository(PostgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Incidentreport>> GetAllReportsAsync()
        {
            return await _context.Incidentreports.ToListAsync();
        }

        public async Task<Incidentreport> GetReportByIdAsync(int id)
        {
            return await _context.Incidentreports.FindAsync(id);
        }

        public async Task AddReportAsync(Incidentreport report)
        {
            _context.Incidentreports.Add(report);
            await _context.SaveChangesAsync();
        }
    }
}
