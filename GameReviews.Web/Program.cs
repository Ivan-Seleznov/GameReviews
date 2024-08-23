using Carter;
using GameReviews.Application;
using GameReviews.Infrastructure;
using GameReviews.Infrastructure.Data.Extensions;
using GameReviews.Web.Extensions;
using GameReviews.Web.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConf) =>
{
    loggerConf.ReadFrom.Configuration(context.Configuration);
});

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthorization();

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
else
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.Run();