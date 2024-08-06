using AeMAPI.Models;
using AeMAPI.Repository;
using AeMAPI.Service;
using Amazon;
using Amazon.SQS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Amazon.Extensions.NETCore.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});


// Add services to the container.
builder.Services.AddDbContext<PostgresContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configura los servicios de AWS
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions()); // Usa el rol IAM en EC2

// Crea una configuración específica para SQS
var sqsConfig = new AmazonSQSConfig
{
    RegionEndpoint = RegionEndpoint.GetBySystemName(builder.Configuration["AWS:Region"])
};

// Registra el cliente de SQS con la configuración específica
builder.Services.AddSingleton<IAmazonSQS>(new AmazonSQSClient(sqsConfig));

builder.Services.AddScoped<IIncidentReportRepository, IncidentReportRepository>();
builder.Services.AddScoped<IIncidentReportService, IncidentReportService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
