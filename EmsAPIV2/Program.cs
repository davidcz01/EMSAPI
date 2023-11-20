using EmsAPIV2.Data;
using EmsAPIV2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<EmsAPIV2Context>(options =>
//    options.UseSqlServer(connectionString));

// NEW SERVICES
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());

});


builder.Services.AddDbContext<EmsAPIV2Context>(options =>
options.UseSqlServer("Server=(local);Database=EmsAPIV2;Trusted_Connection=True;TrustServerCertificate=True;"));

    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowOrigin");


//ADD DBCONTEXT, LOAD DEPARTMENTS
using (var context = new EmsAPIV2Context())
{
    var departmentHR = context.Departments.FirstOrDefault(d => d.DepartmentID == 1);
    var departmentIT = context.Departments.FirstOrDefault(d => d.DepartmentID == 2);
    var departmentFinance = context.Departments.FirstOrDefault(d => d.DepartmentID == 3);
    var departmentMarketing = context.Departments.FirstOrDefault(d => d.DepartmentID == 4);
    
    context.Entry(departmentHR).Collection(d => d.Employees).Load();
    context.Entry(departmentIT).Collection(d => d.Employees).Load();
    context.Entry(departmentFinance).Collection(d => d.Employees).Load();
    context.Entry(departmentMarketing).Collection(d => d.Employees).Load();

    //POPULATE DATABASE

    //var employee1 = new Employee
    //{
    //    DepartmentID = departmentHR.DepartmentID, // Use the DepartmentID from the created department
    //    DepartmentName = "HR",
    //    FirstName = "John",
    //    Surname = "Doe",
    //    Email = "john.doe@example.com",
    //    Date = DateTime.Now,
    //    HolidayEntitlement = 20
    //};
    //context.Employees.Add(employee1);

    //var employee2 = new Employee
    //{
    //    DepartmentID = departmentIT.DepartmentID, // Use the DepartmentID from the created department
    //    DepartmentName = "IT",
    //    FirstName = "Jane",
    //    Surname = "Doe",
    //    Email = "jane.doe@gmail.com",
    //    Date = DateTime.Now,
    //    HolidayEntitlement = 12
    //};
    //context.Employees.Add(employee2);

    //var employee3 = new Employee
    //{
    //    DepartmentID = departmentFinance.DepartmentID, // Use the DepartmentID from the created department
    //    DepartmentName = "Finance",
    //    FirstName = "David",
    //    Surname = "Cziraki",
    //    Email = "davidcziraki@outlook.com",
    //    Date = DateTime.Now,
    //    HolidayEntitlement = 100
    //};
    //context.Employees.Add(employee3);

    //var employee4 = new Employee
    //{
    //    DepartmentID = departmentMarketing.DepartmentID, // Use the DepartmentID from the created department
    //    DepartmentName = "Marketing",
    //    FirstName = "Example",
    //    Surname = "Example",
    //    Email = "placeholder@example.com",
    //    Date = DateTime.Now,
    //    HolidayEntitlement = 0
    //};
    //context.Employees.Add(employee4);

    departmentHR.UpdateEmployeeCount();
    departmentIT.UpdateEmployeeCount();
    departmentMarketing.UpdateEmployeeCount();
    departmentFinance.UpdateEmployeeCount();

    context.SaveChanges();
}



app.Run();
