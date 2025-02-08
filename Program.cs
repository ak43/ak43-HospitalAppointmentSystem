using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Filters;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Repositories;
using HospitalAppointmentSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(option => 
option.Filters.Add(new MyLogging()));
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//REgister AutoMapper with an application's Dependency Injection (DI)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ApptCorsPolicy", builder => builder
    //.WithOrigins("http://localhost:5187")
    .WithOrigins("*")
    .WithMethods("POST")
    .AllowAnyHeader());
    //.WithHeaders("Accept", "Authorization", "x-api-version"));
    //.WithHeaders("Authorization", "Content-Type"));
    options.AddPolicy("AllowAllOrigin", builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

// Step 1: Configure Serilog as the logger for the app
// Customize Log Leve: Default: WriteTo.Console()
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Use Serilog for logging
builder.Host.UseSerilog();

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Register JwtService
builder.Services.AddScoped<JwtService>();

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ApptCorsPolicy");


//app.UseCors("AllowAllOrigin");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

