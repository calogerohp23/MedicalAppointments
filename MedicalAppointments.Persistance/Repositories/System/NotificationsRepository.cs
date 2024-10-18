using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.System;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class NotificationsRepository : BaseRepository<Notifications>, INotificationsRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<NotificationsRepository> logger;

        public NotificationsRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<NotificationsRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Notifications entity)
        {
            OperationResult operationResult = new OperationResult();

            return operationResult;
        }

        public async override Task<OperationResult> Update(Notifications entity)
        {
            OperationResult operationResult = new OperationResult();
            return operationResult;
        }

        public async override Task<OperationResult> Remove(Notifications enitity)
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