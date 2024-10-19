using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Users;
using MedicalAppointments.Persistance.Repositories.Appointments;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace MedicalAppointments.Persistance.Repositories.Users
{
    public class UsersRepository : BaseRepository<Appointment>, IUsersRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<AppointmentRepository> logger;
        public UsersRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<AppointmentRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }
        public Task<OperationResult> Save(Domain.Entities.Users.Users entity)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(Domain.Entities.Users.Users entity)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(Domain.Entities.Users.Users entity)
        {
            throw new NotImplementedException();
        }


        }

        public Task<bool> Exists(Expression<Func<Domain.Entities.Users.Users, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
