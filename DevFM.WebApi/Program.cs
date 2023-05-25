using DevFM.SqlServerAdapter;
using DevFM.WebApi;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DevFM.Application.Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(WebApiMapperProfile));

builder.Services.AddSwaggerGen();

builder.Services.AddApplicationService();

builder.Services.AddSingleton<SqlServerAdapterConfiguration>();
var configura = new SqlServerAdapterConfiguration();
configura.SqlConnectionString = "Data Source=MILTEC0494\\LOCALSQL;Initial Catalog=BD_Base;Integrated Security=True";
builder.Services.AddSqlServerAdapter(configura);
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
