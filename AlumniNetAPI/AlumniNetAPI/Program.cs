using AlumniNetAPI.Models;
using AlumniNetAPI.Repository;
using AlumniNetAPI.Repository.Interfaces;
using AlumniNetAPI.Services;
using Amazon.S3;
using FirebaseAdmin;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("FIREBASE-CONFIG"))
}));

builder.Services.AddFirebaseAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AlumniNetAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyContext")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IFileStorageService, S3FileStorageService>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
