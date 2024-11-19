using MedicalAppointments.Persistance.Interfaces.Medical;
using MedicalAppointments.Persistance.Repositories.Medical;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalAppointment.IOC.Dependencies.Medical
{
    public static class MedicalDependency
    {
        public static void AddMedicalDependency(this IServiceCollection services)
        {
            services.AddScoped<IAvailabilityModesRepository, AvailabilityModesRepository>();
            services.AddScoped<IMedicalRecordsRepository, MedicalRecordsRepository>();
            services.AddScoped<ISpecialtiesRepository, SpecialtiesRepositroy>();
        }
    }
}
