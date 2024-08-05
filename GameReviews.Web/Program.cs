using Carter;
using GameReviews.Application;
using GameReviews.Infrastructure;
using GameReviews.Infrastructure.Data.Extensions;
using GameReviews.Web.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConf) =>
{
    loggerConf.ReadFrom.Configuration(context.Configuration);
});

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandlers();

builder.Services.AddCarter();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ApplyMigrations();
}

app.UseExceptionHandler();
app.UseSerilogRequestLogging();

app.MapCarter();

app.Run();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            