using efcore_bulk_insert_example.Context;
using efcore_bulk_insert_example.Extensions;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
var defaultDBConnection = "Server=localhost; Database=efcorebulk; Uid=admin;Pwd=root; SSL Mode=None; AllowLoadLocalInfile=true";

Console.WriteLine($"Connectionstring: {defaultDBConnection}");

builder.Services.AddDbContext<BulkExampleContext>(options =>
{
    options
        .UseMySql(defaultDBConnection, new MySqlServerVersion(new Version(8, 0, 32)), opt => {   
            opt.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
