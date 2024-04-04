using Api.AutoMapper;
using Core.Connection.HttpLogic;
using Core.Connection.RabbitRpcLogic.Services;
using Core.Connection.RabbitRpcLogic.Services.Interfaces;
using Core.Logs;
using Dal;
using TestApiConnectionLib.TestApiConnectionServices;
using Logic;
using Logic.AutoMapper;
using Core.TraceIdLogic;
using Core.TraceIdLogic.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.TryAddDal();
builder.Services.TryAddLogic();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(LogicToDalMappingProfile), typeof(LogicToApiMappingProfile));
builder.Services.AddHttpServiceStartUp();
builder.Services.AddRabbitService();
builder.Services.AddTestApiConnectionServices(builder.Configuration);
builder.Services.TryAddTraceId();
builder.Services.AddLoggerServices();
builder.Host.UseSerilog((context, configuration) => configuration.GetConfiguration());
    
var app = builder.Build();

app.UseMiddleware<ReadTraceIdMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();