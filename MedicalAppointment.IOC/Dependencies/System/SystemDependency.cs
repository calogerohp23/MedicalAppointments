using MedicalAppointments.Persistance.Interfaces.System;
using MedicalAppointments.Persistance.Repositories.System;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalAppointment.IOC.Dependencies.System
{
    public static class SystemDependency
    {
        public static void AddSystemDependency(this IServiceCollection services)
        {
            services.AddScoped<INotificationsRepository, NotificationsRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
        }
    }
}
