using Application.Playground;
using Application.ScriptEditor;
using Db;
using DotNetCore.AspNetCore;
using DotNetCore.IoC;
using DotNetCore.Logging;
using DotNetCore.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

builder.Host.UseSerilog();

builder.Services.AddControllers().AddJsonOptions();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSpaStaticFiles("Frontend");
builder.Services.AddContext<AdventureContext>(options => options.UseSqlServer(builder.Services.GetConnectionString(nameof(AdventureContext))));

// Application services
builder.Services.AddScoped<IAdventurePlaygroundService, AdventurePlaygroundService>();
builder.Services.AddScoped<IScriptEditorService, ScriptEditorService>();
builder.Services.AddScoped<IAdventurePlaygroundService, AdventurePlaygroundService>();
//

var application = builder.Build();

application.UseException();
application.UseRouting();
application.UseEndpointsMapControllers();
application.UseSwagger();
application.UseSwaggerUI();
application.UseSpaAngular("Frontend", "start", "http://localhost:4200");

application.Run();
