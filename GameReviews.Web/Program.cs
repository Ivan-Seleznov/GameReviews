using Carter;
using GameReviews.Application;
using GameReviews.Application.Common.Models.Dtos;
using GameReviews.Application.Users.Commands.CreateUser;
using GameReviews.Application.Users.Queries.GetUser;
using GameReviews.Domain.Entities.User;
using GameReviews.Infrastructure;
using GameReviews.Infrastructure.Data.Extensions;
using GameReviews.Web.Endpoints;
using GameReviews.Web.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
}

app.UseExceptionHandler();
app.UseSerilogRequestLogging();
await app.UseDbContext();

app.MapCarter();

app.Run();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       