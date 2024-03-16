using Api.AutoMapper;
using Core.HttpLogic;
using Core.Logs;
using Core.Logs.Middleware;
using Dal;
using TestApiConnectionLib.TestApiConnectionServices;
using Logic;
using Logic.AutoMapper;
using Core.TraceIdLogic;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.TryAddDal();
builder.Services.TryAddLogic();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(LogicToDalMappingProfile), typeof(LogicToApiMappingProfile));
builder.Services.AddHttpServiceStartUp();
builder.Services.AddTestApiConnectionServices();
builder.Services.TryAddTraceId();
builder.Services.AddLoggerServices();
builder.Host.UseSerilog((context, configuration) => configuration.GetConfiguration());
    
var app = builder.Build();

// app.UseMiddleware<Core.Logs.Middleware.LogTraceIdMiddleware>();
// Configure the HTTP request pipeline.
app.UseMiddleware<LogTraceIdMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();