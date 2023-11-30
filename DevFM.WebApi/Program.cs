using DevFM.SqlServerAdapter;
using DevFM.WebApi;
using DevFM.Application.Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddCors(options =>
//{
//    //options.AddPolicy(name: MyAllowAllSpecificOrigins,
//    //    policy =>
//    //    {
//    //        policy.WithOrigins("http://fmaia.somee.com","http://api.fmaia.somee.com")
//    //        .AllowAnyOrigin()
//    //        .AllowAnyMethod()
//    //        .AllowAnyHeader();
//    //    });
//    options.AddPolicy("Production",
//                   builder =>
//                       builder
//                           .WithMethods("GET")
//                           .WithOrigins("https://desenvolvimento.sistemagerenciador.com.br/devapi")
//                           .SetIsOriginAllowedToAllowWildcardSubdomains()
//                           //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
//                           .AllowAnyHeader());
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(WebApiMapperProfile));

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

});

builder.Services.AddApplicationService();

builder.Services.AddSingleton<SqlServerAdapterConfiguration>();
var configura = new SqlServerAdapterConfiguration();
configura.SqlConnectionString = "workstation id=bd.iron.hostazul.com.br,3533;packet size=4096;user id=174_fmaia;pwd=dbcu1dad@or;data source=bd.iron.hostazul.com.br,3533;persist security info=False;initial catalog=174_dbcuidador";
//configura.SqlConnectionString = "Data Source=MILTEC0494\\LOCALSQL;Initial Catalog=BD_Base;Integrated Security=True";
//configura.SqlConnectionString = "server=localhost;User Id=root;database=dbcuidador; password=Chico01020122";
//configura.SqlConnectionString = "server=bd.asp.hostazul.com.br;Port=4406;User Id=15467_teste;database=15467_testechico; password=Teste2023";
//configura.SqlConnectionString = "workstation id=dbcuidador.mssql.somee.com;packet size=4096;user id=fmaia_SQLLogin_1;pwd=7mb1dfxdyf;data source=dbcuidador.mssql.somee.com;persist security info=False;initial catalog=dbcuidador";


builder.Services.AddSqlServerAdapter(configura);
builder.Services.AddHealthChecks();

var key = Encoding.ASCII.GetBytes(Key.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();




app.UseHttpsRedirection();


//app.UseAuthorization();

//app.UseCors("Production"); // Usar apenas nas demos => Configuração Ideal: Production
//app.UseCors(); 
app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});
app.UseHsts();
//Obrigatoriamente nessa ordem
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
