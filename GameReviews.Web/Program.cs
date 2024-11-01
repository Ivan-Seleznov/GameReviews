using Carter;
using GameReviews.Application;
using GameReviews.Application.Common.Logger;
using GameReviews.Domain;
using GameReviews.Infrastructure;
using GameReviews.Infrastructure.Data.Extensions;
using GameReviews.Web.Extensions;
using GameReviews.Web.Middleware;
using GameReviews.Web.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConf) =>
{
    loggerConf.ReadFrom.Configuration(context.Configuration);
});

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddDomain();

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
builder.Services.AddMetrics();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
var app = builder.Build();
MethodTimeLogger.Logger = app.Logger;

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

app.UseCors("AllowAllOrigins"); // Enable CORS

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SaveUserIdMiddleware>();

app.MapCarter();

app.Run();