using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Insurance;
using MedicalAppointments.Persistance.Models.Insurance;
using MedicalAppointments.Persistance.Validators.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class NetworkTypeRepository : BaseRepository<NetworkType>, INetworkTypeRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<NetworkTypeRepository> logger;
        private readonly NetworkTypeValidator _validator;

        public NetworkTypeRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<NetworkTypeRepository> logger, NetworkTypeValidator validator) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _validator = validator;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(NetworkType entity)
        {
            OperationResult operationResult = new();

            _validator.ValidateNull(entity);
            _validator.ValidateSave(entity);

            try
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;

                await base.Save(entity);

                operationResult.Success = true;
                operationResult.Message = "The network type was saved successfully!";

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the network type.";
                this.logger.LogError(operationResult.Message, ex.ToString());

            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id, NetworkType entity)
        {
            OperationResult operationResult = new();

            _validator.ValidateNull(entity);
            _validator.ValidateID(id);
            _validator.ValidateUpdate(id, entity);

            try
            {
                NetworkType? networkTypeToUpdate = await _medicalAppointmentContext.NetworkType.FindAsync(id);

                networkTypeToUpdate.Name = entity.Name;
                networkTypeToUpdate.Description = entity.Description;
                networkTypeToUpdate.UpdatedAt = DateTime.Now;
                networkTypeToUpdate.UpdatedBy = entity.UpdatedBy;

                await _medicalAppointmentContext.SaveChangesAsync();

                operationResult.Success = true;
                operationResult.Message = "The network type was successfully updated!";

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error updating the network type.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(int id, NetworkType entity)
        {
            OperationResult operationResult = new();

            _validator.ValidateNull(entity);
            _validator.ValidateID(id);
            _validator.ValidateRemove(id, entity);
            try
            {
                NetworkType? networkTypeToRemove = await _medicalAppointmentContext.NetworkType.FindAsync(id);

                networkTypeToRemove.IsActive = false;
                networkTypeToRemove.UpdatedAt = DateTime.Now;
                networkTypeToRemove.UpdatedBy = entity.UpdatedBy;

                await _medicalAppointmentContext.SaveChangesAsync();

                operationResult.Success = true;
                operationResult.Message = "The network type was disabled successfully!";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error removing the network type.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from networkType in _medicalAppointmentContext.NetworkType
                                              join createdUser in _medicalAppointmentContext.Users on networkType.CreatedBy equals createdUser.UserID
                                              join updatedUser in _medicalAppointmentContext.Users on networkType.UpdatedBy equals updatedUser.UserID
                                              where networkType.IsActive == true
                                              orderby networkType.CreatedAt descending
                                              select new NetworkTypeModel()
                                              {
                                                  NetworkTypeID = networkType.NetworkTypeID,
                                                  Name = networkType.Name,
                                                  Description = networkType.Description,
                                                  CreatedAt = networkType.CreatedAt,
                                                  UpdatedAt = networkType.UpdatedAt,
                                                  IsActive = networkType.IsActive,
                                                  CreatedBy = createdUser.FirstName + " " + createdUser.LastName,
                                                  UpdatedBy = updatedUser.FirstName + " " + updatedUser.LastName,
                                              }).AsNoTracking()
                                             .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error getting all the network types.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new();

            _validator.ValidateID(id);
            try
            {
                operationResult.Data = await (from networkType in _medicalAppointmentContext.NetworkType
                                              join createdUser in _medicalAppointmentContext.Users on networkType.CreatedBy equals createdUser.UserID
                                              join updatedUser in _medicalAppointmentContext.Users on networkType.UpdatedBy equals updatedUser.UserID
                                              where networkType.IsActive == true && networkType.NetworkTypeID == id
                                              orderby networkType.CreatedAt descending
                                              select new NetworkTypeModel()
                                              {
                                                  NetworkTypeID = networkType.NetworkTypeID,
                                                  Name = networkType.Name,
                                                  Description = networkType.Description,
                                                  CreatedAt = networkType.CreatedAt,
                                                  UpdatedAt = networkType.UpdatedAt,
                                                  IsActive = networkType.IsActive,
                                                  CreatedBy = createdUser.FirstName + " " + createdUser.LastName,
                                                  UpdatedBy = updatedUser.FirstName + " " + updatedUser.LastName,
                                              }).AsNoTracking()
                             .ToListAsync();
                operationResult.Data = _validator.ValidateNullData(operationResult.Data);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error getting all the Networks";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }


    }
}
