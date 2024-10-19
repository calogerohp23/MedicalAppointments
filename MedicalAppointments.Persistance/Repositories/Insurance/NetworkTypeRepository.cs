using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Insurance;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class NetworkTypeRepository : BaseRepository<NetworkType>, INetworkTypeRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<NetworkTypeRepository> logger;

        public NetworkTypeRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<NetworkTypeRepository> logger ) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(NetworkType entity)
        {
            OperationResult operationResult = new OperationResult();

            return operationResult;
        }

        public async override Task<OperationResult> Update(NetworkType entity)
        {
            OperationResult operationResult = new OperationResult();
            return operationResult;
        }

        public async override Task<OperationResult> Remove(NetworkType enitity)
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
