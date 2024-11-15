using MedicalAppointments.Persistance.Interfaces.Users;
using MedicalAppointments.Persistance.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;
namespace MedicalAppointment.IOC.Dependencies.Users
{
    public static class UsersDependency
    {
        public static void AddUsersDependency(this IServiceCollection service)
        {
            service.AddScoped<IDoctorsRepository, DoctorsRepository>();
            service.AddScoped<IPatientsRepository, PatientsRepository>();
            service.AddScoped<IUsersRepository, UsersRepository>();
            //service.AddTransient<>
        }
    }
}
