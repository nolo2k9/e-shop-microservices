var builder = WebApplication.CreateBuilder(args);
//Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(
        builder.Configuration
        .GetConnectionString("DatabaseConnection")!);
}).UseLightweightSessions();

var app = builder.Build();

//configure the http request pipeline
app.MapCarter();
app.UseHttpsRedirection();


app.Run();
