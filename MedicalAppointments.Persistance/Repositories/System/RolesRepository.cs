using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.System;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class RolesRepository : BaseRepository<Roles>, IRolesRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<RolesRepository> logger;

        public RolesRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<RolesRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Roles entity)
        {
            OperationResult operationResult = new OperationResult();

            return operationResult;
        }

        public async override Task<OperationResult> Update(Roles entity)
        {
            OperationResult operationResult = new OperationResult();
            return operationResult;
        }

        public async override Task<OperationResult> Remove(Roles enitity)
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