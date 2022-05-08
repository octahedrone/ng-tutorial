using DotNetCore.AspNetCore;
using DotNetCore.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder();

builder.Host.UseSerilog();

builder.Services.AddControllers().AddJsonOptions();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSpaStaticFiles("Frontend");

var application = builder.Build();

application.UseException();
application.UseRouting();
application.UseEndpointsMapControllers();
application.UseSwagger();
application.UseSwaggerUI();
application.UseSpaAngular("Frontend", "start", "http://localhost:4200");

application.Run();
