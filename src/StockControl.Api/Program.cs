using MySql.Data.MySqlClient;
using Serilog;
using StockControl.Api.Extensions;
using StockControl.Api.Middleware;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

if (builder.Configuration.GetValue<bool>("EnableSerilogSelfLog", false))
{
    Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"Serilog: {msg}"));
}

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connection = new MySqlConnection(builder.Configuration.GetConnectionString("ConnectionString"));
    connection.Open();
    return connection;
});

builder.Services.AddCustomMediatR();
builder.Services.AddServices();


builder.Host.UseSerilog((context, services, configuration) =>
    configuration.WriteTo.Console()
                .WriteTo.MySQL(
                    context.Configuration.GetConnectionString("ConnectionString"),
                    tableName: "Logs",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
                 )
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
