using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Insurance;
using MedicalAppointments.Persistance.Models.Insurance;
using Microsoft.EntityFrameworkCore;
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

            try
            {
                await base.Save(entity);
            }
            catch (Exception ex) 
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the Network";
                this.logger.LogError(operationResult.Message, ex);
            
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(NetworkType entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                NetworkType? networkTypeToUpdate = await _medicalAppointmentContext.NetworkType.FindAsync(entity.NetworkTypeID);
                
                networkTypeToUpdate.Name = entity.Name;
                networkTypeToUpdate.Description = entity.Description;
                networkTypeToUpdate.CreatedAt = entity.CreatedAt;
                networkTypeToUpdate.UpdatedAt = entity.UpdatedAt;
                networkTypeToUpdate.IsActive = entity.IsActive;
                networkTypeToUpdate.CreatedBy = entity.CreatedBy;
                networkTypeToUpdate.UpdatedBy = entity.UpdatedBy;

                await base.Update(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error updating the Network";
                this.logger.LogError(operationResult.Message, ex);

            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(NetworkType enitity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                NetworkType? networkTypeToRemove = await _medicalAppointmentContext.NetworkType.FindAsync(enitity.NetworkTypeID);

                networkTypeToRemove.IsActive = false;
                networkTypeToRemove.UpdatedAt = enitity.UpdatedAt;
                networkTypeToRemove.UpdatedBy = enitity.UpdatedBy;
            }
            catch (Exception ex) 
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error removing the Network";
                this.logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                operationResult.Data = await (from networkType in _medicalAppointmentContext.NetworkType
                                              where networkType.IsActive == true
                                              orderby networkType.CreatedAt descending
                                              select new NetworkTypeModel()
                                              {
                                                  NetworkTypeID = networkType.NetworkTypeID,
                                                  Name = networkType.Name,
                                                  Description = networkType.Description,
                                                  CreatedAt = networkType.CreatedAt
                                              }).AsNoTracking()
                                             .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error getting all the Networks";
                this.logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                operationResult.Data = await (from networkType in _medicalAppointmentContext.NetworkType
                                              where networkType.IsActive == true && networkType.NetworkTypeID == id
                                              orderby networkType.CreatedAt descending
                                              select new NetworkTypeModel()
                                              {
                                                  NetworkTypeID = networkType.NetworkTypeID,
                                                  Name = networkType.Name,
                                                  Description = networkType.Description,
                                                  CreatedAt = networkType.CreatedAt,
                                                  CreatedBy = networkType.CreatedBy,
                                              }).AsNoTracking()
                             .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error getting all the Networks";
                this.logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }


    }
}
