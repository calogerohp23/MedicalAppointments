using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Users;
using MedicalAppointments.Persistance.Repositories.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MedicalAppointmentContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointmentDB")));

// Dependencies Registry
builder.Services.AddScoped<IDoctorsRepository, DoctorsRepository>();
builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
// Services Registry

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
