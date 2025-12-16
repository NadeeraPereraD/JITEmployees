using FluentValidation;
using FluentValidation.AspNetCore;
using JITEmployees.API.Infrastructure.DependencyInjection;
using JITEmployees.API.Services.Validations.Departments;
using JITEmployees.API.Services.Validations.Employees;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddValidatorsFromAssembly(typeof(CreateEmployeeValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(UpdateEmployeeValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(CreateDepartmentValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(UpdateDepartmentValidator).Assembly);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:5173") 
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
