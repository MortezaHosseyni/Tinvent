using Tinvent.Application;
using Tinvent.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
