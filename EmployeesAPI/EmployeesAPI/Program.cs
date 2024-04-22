using EmployeesAPI.Data.Abstractions;
using EmployeesAPI.Data.Context;
using EmployeesAPI.Domain.Abstractions;
using EmployeesAPI.Domain.Handlers;
using EmployeesAPI.Domain.Repositories;
using EmployeesAPI.Domain.Services;
using EmployeesAPI.Extensions;
using EmployeesAPI.Profiles;
using FluentMigrator.Runner;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPassportRepository, PassportRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb.AddPostgres().WithGlobalConnectionString(connectionString).ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
    .AddLogging(rb => rb.AddFluentMigratorConsole());

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.02",
        Title = "Employees API",
        Description = "АPI для записи и хранения данных о сотрудниках и их получением.",
        Contact = new OpenApiContact()
        {
            Name = "Сергей Акмашев",
            Email = "akmashev2002@gmail.com"
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Migrate();

app.Run();

