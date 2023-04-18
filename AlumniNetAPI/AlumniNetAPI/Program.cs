using AlumniNetAPI.Repository.Interfaces;
using AlumniNetAPI.Repository;
using Microsoft.EntityFrameworkCore;
using AlumniNetAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using FirebaseAdmin;
using AlumniNetAPI.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddSingleton(FirebaseApp.Create());

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>
//    (JwtBearerDefaults.AuthenticationScheme, (o) => { });

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AlumniNetAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyContext")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
