using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using NetCore.AutoRegisterDi;
using TestMySql;
using TestMySql.Entities;
using TestMySql.Exceptions;
using TestMySql.Repositories.Class;
using TestMySql.Repositories.Interface;
using TestMySql.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDbContext<StudentCourseDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Default"),
        new MySqlServerVersion(new Version(9, 1, 0))
    )
);
builder.Services.RegisterAssemblyPublicNonGenericClasses()
    .Where(c => c.Name.EndsWith("Service") || c.Name.EndsWith("Repository"))
    .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Use custom exception middleware
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
