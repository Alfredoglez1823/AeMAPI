using AeMAPI.Models;
using AeMAPI.Repository;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;


namespace AeMAPI.Service
{
    public class IncidentReportService : IIncidentReportService
    {
        private readonly IIncidentReportRepository _repository;
        private readonly IAmazonSQS _sqs;
        private readonly string _queueUrl;

        public IncidentReportService(IIncidentReportRepository repository, IAmazonSQS sqs, IConfiguration configuration)
        {
            _repository = repository;
            _sqs = sqs;
            _queueUrl = configuration["AWS:QueueUrl"];
        }

        public async Task<IEnumerable<Incidentreport>> GetAllReportsAsync()
        {
            return await _repository.GetAllReportsAsync();
        }

        public async Task<Incidentreport> GetReportByIdAsync(int id)
        {
            return await _repository.GetReportByIdAsync(id);
        }

        public async Task AddReportAsync(Incidentreport report)
        {
            await _repository.AddReportAsync(report);

            var messageBody = JsonSerializer.Serialize(report);
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = messageBody
            };

            await _sqs.SendMessageAsync(sendMessageRequest);
        }
    }
}
