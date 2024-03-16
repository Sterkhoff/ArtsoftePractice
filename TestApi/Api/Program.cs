using Api.AutoMapper.QuestionMappingProfile;
using Api.AutoMapper.TestMappingProfile;
using Api.AutoMapper.UserMappingProfile;
using Core.TraceIdLogic;
using Core.Logs;
using Infrastructure;
using Serilog;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(QuestionMappingProfile), typeof(TestMappingProfile), typeof(UserMappingProfile));
builder.Services.TryAddServices();
builder.Services.TryAddInfrastructure();
builder.Services.TryAddTraceId();
builder.Services.AddLoggerServices();
builder.Host.UseSerilog((context, configuration) => configuration.GetConfiguration());

var app = builder.Build();

app.UseMiddleware<ReadTraceIdMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();