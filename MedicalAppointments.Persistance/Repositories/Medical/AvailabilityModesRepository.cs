using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Medical;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class AvailabilityModesRepository : BaseRepository<AvailabilityModes>, IAvailabilityModesRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<AvailabilityModesRepository> logger;

        public AvailabilityModesRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<AvailabilityModesRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(AvailabilityModes entity)
        {
            OperationResult operationResult = new OperationResult();

            return operationResult;
        }

        public async override Task<OperationResult> Update(AvailabilityModes entity)
        {
            OperationResult operationResult = new OperationResult();
            return operationResult;
        }

        public async override Task<OperationResult> Remove(AvailabilityModes enitity)
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