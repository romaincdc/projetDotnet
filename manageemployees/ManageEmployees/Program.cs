using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("EmployeesDatabase");

//Gestion CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Connexion � la base de donn�es
builder.Services.AddDbContext<ManageEmployeeDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Ajout des repositories
builder.Services.AddScoped<IDepartementRepository, DepartementRepository>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

// Ajout des services
builder.Services.AddScoped<IDepartementService, DepartementService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequesteService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://example.com",
                                              "http://www.contoso.com",
                                              "file:///Users/romaincarvalho/Documents/Cours%20N5%2023-24/dotnet/test2/FrontEnd/index.html");
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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
