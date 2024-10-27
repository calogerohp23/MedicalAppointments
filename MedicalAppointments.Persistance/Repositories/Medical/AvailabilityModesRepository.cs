using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Medical;
using MedicalAppointments.Persistance.Models.Medical;
using Microsoft.EntityFrameworkCore;
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
            OperationResult operationResult = new();
            if(entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
                return operationResult;
            }
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The availability mode couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(AvailabilityModes entity)
        {
            OperationResult operationResult = new();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
                return operationResult;
            }
            try
            {
                AvailabilityModes? availabilityModesToUpdate = await _medicalAppointmentContext.AvailabilityModes.FindAsync(entity.SAvailabilityModeID);
                availabilityModesToUpdate.SAvailabilityModeID = entity.SAvailabilityModeID;
                availabilityModesToUpdate.AvailabilityMode = entity.AvailabilityMode;
                availabilityModesToUpdate.UpdatedAt = entity.UpdatedAt;
                availabilityModesToUpdate.UpdatedBy = entity.UpdatedBy;

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The availability mode couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(AvailabilityModes entity)
        {
            OperationResult operationResult = new();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
                return operationResult;
            }
            try
            {
                AvailabilityModes? availabilityModesToRemove = await _medicalAppointmentContext.AvailabilityModes.FindAsync(enitity.SAvailabilityModeID);
                availabilityModesToRemove.IsActive = false;
                availabilityModesToRemove.UpdatedAt = entity.UpdatedAt;
                availabilityModesToRemove.UpdatedBy = entity.UpdatedBy;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The availability mode couldn't be removed.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from availabilityModes in _medicalAppointmentContext.AvailabilityModes
                                              where availabilityModes.IsActive == true
                                              orderby availabilityModes.SAvailabilityModeID descending
                                              select new AvailabilityModesModel()
                                              {
                                                  SAvailabilityModeID = availabilityModes.SAvailabilityModeID,
                                                  AvailabilityMode = availabilityModes.AvailabilityMode,
                                                  CreatedAt = availabilityModes.CreatedAt,
                                                  CreatedBy = availabilityModes.CreatedBy,
                                                  UpdatedAt = availabilityModes.UpdatedAt,
                                                  UpdatedBy = availabilityModes.UpdatedBy,
                                              }).AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaining all the availability modes.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from availabilityModes in _medicalAppointmentContext.AvailabilityModes
                                              where availabilityModes.IsActive == true && availabilityModes.SAvailabilityModeID == id
                                              orderby availabilityModes.SAvailabilityModeID descending
                                              select new AvailabilityModesModel()
                                              {
                                                  SAvailabilityModeID = availabilityModes.SAvailabilityModeID,
                                                  AvailabilityMode = availabilityModes.AvailabilityMode,
                                                  CreatedAt = availabilityModes.CreatedAt,
                                                  CreatedBy = availabilityModes.CreatedBy,
                                                  UpdatedAt = availabilityModes.UpdatedAt,
                                                  UpdatedBy = availabilityModes.UpdatedBy,
                                              }).AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaining all the availability modes.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}