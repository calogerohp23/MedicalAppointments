using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Medical;
using MedicalAppointments.Persistance.Repositories.Medical;
using MedicalAppointments.Persistance.Validators.Medical;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MedicalAppointmentContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointmentDb")));

// Dependencios Registry
builder.Services.AddScoped<IAvailabilityModesRepository, AvailabilityModesRepository>();
builder.Services.AddScoped<AvailabilityModesValidator>();
builder.Services.AddScoped<IMedicalRecordsRepository, MedicalRecordsRepository>();
builder.Services.AddScoped<MedicalRecordsValidator>();
builder.Services.AddScoped<ISpecialtiesRepository, SpecialtiesRepositroy>();
builder.Services.AddScoped<SpecialtiesValidator>();
// Services Registry


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
