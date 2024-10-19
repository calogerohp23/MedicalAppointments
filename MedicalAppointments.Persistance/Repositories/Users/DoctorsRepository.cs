using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Users;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class DoctorsRepository : BaseRepository<Doctors>, IDoctorsRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<DoctorsRepository> logger;

        public DoctorsRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<DoctorsRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Doctors entity)
        {
            OperationResult operationResult = new OperationResult();

            return operationResult;
        }

        public async override Task<OperationResult> Update(Doctors entity)
        {
            OperationResult operationResult = new OperationResult();
            return operationResult;
        }

        public async override Task<OperationResult> Remove(Doctors enitity)
        {
            OperationResult operationResult = new OperationResult();
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new OperationResult();
            return operationResult;
        }
    }
}