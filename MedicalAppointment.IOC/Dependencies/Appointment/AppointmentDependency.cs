using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Persistance.Interfaces.Appointment;
using MedicalAppointments.Persistance.Repositories.Appointments;
using MedicalAppointments.Persistance.Validators.Appointments;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalAppointment.IOC.Dependencies.Appointment
{
    public static class AppointmentDependency
    {
        public static void AddAppointmentDependency(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepository,AppointmentRepository>();
            services.AddScoped<AppointmentValidator>();
            services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
            services.AddScoped<DoctorAvailabilityValidator>();
        }
    }
}
