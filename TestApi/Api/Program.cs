using Api.AutoMapper.QuestionMappingProfile;
using Api.AutoMapper.TestMappingProfile;
using Api.AutoMapper.UserMappingProfile;
using Infrastructure;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();