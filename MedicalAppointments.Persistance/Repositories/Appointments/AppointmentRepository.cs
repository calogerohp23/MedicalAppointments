using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Appointments
{
    public class AppointmentRepository : BaseRepository<Appointment>, Interfaces.Appointments.IAppointmentRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<AppointmentRepository> logger;
        public AppointmentRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<AppointmentRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Appointment entity)
        {
            EntityValidator<Appointment> validator = new EntityValidator<Appointment>;
            OperationResult operationResult = new OperationResult();
            
            validator.ValidateNulls(entity);

            validator.ValidateLessEqualZero();
            validator.
            validator.ValidateEqualZero()
            try
            {
                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = ex.Message;
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }


    }
}
