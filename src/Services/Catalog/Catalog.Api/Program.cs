using HealthChecks.UI.Client;
using JasperFx;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));

});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(
        builder.Configuration
        .GetConnectionString("DatabaseConnection")!);
    opts.AutoCreateSchemaObjects = AutoCreate.All;
}).UseLightweightSessions();

builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
.AddNpgSql(builder.Configuration.GetConnectionString("DatabaseConnection")!);

var app = builder.Build();

app.UseExceptionHandler(options => { });
app.UseHttpsRedirection();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
//configure the http request pipeline
app.MapCarter();
app.Run();
