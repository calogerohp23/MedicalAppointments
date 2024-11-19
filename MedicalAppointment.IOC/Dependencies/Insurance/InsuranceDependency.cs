using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Persistance.Interfaces.Insurance;
using MedicalAppointments.Persistance.Repositories.Insurance;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalAppointment.IOC.Dependencies.Insurance
{
    public static class InsuranceDependency
    {
        public static void AddInsuranceDependency(this IServiceCollection services)
        {
            services.AddScoped<IInsuranceProvidersRepository, InsuranceProvidersRepository>();
            services.AddScoped<INetworkTypeRepository, NetworkTypeRepository>();
        }
    }
}
