using AeMAPI.Models;
using AeMAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentReportController : ControllerBase
    {
        private readonly IIncidentReportService _service;

        public IncidentReportController(IIncidentReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incidentreport>>> GetAllReports()
        {
            var reports = await _service.GetAllReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Incidentreport>> GetReportById(int id)
        {
            var report = await _service.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPost]
        public async Task<ActionResult> AddReport(Incidentreport report)
        {
            await _service.AddReportAsync(report);
            return CreatedAtAction(nameof(GetReportById), new { id = report.Id }, report);
        }

        [HttpOptions]
        public IActionResult Options()
        {
            // Utiliza la política CORS definida en tu program.cs
            Response.Headers.Add("Access-Control-Allow-Origin", "_myAllowSpecificOrigins");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

            // Devuelve una respuesta 200 OK para indicar que la solicitud OPTIONS fue exitosa
            return Ok();
        }
    }
}
