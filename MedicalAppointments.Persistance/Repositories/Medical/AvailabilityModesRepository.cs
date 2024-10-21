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
            OperationResult operationResult = new OperationResult();
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The availability mode couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(AvailabilityModes entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                AvailabilityModes? availabilityModesToUpdate = await _medicalAppointmentContext.AvailabilityModes.FindAsync(entity.SAvailabilityModeID);
                availabilityModesToUpdate.SAvailabilityModeID = entity.SAvailabilityModeID;
                availabilityModesToUpdate.AvailabilityMode = entity.AvailabilityMode;
                availabilityModesToUpdate.CreatedAt = entity.CreatedAt;
                availabilityModesToUpdate.UpdatedAt = entity.UpdatedAt;
                availabilityModesToUpdate.CreatedBy = entity.CreatedBy;
                availabilityModesToUpdate.UpdatedBy = entity.UpdatedBy;

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The availability mode couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(AvailabilityModes enitity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                AvailabilityModes? availabilityModesToRemove = await _medicalAppointmentContext.AvailabilityModes.FindAsync(enitity.SAvailabilityModeID);
                availabilityModesToRemove.IsActive = false;
                availabilityModesToRemove.UpdatedAt = enitity.UpdatedAt;
                availabilityModesToRemove.UpdatedBy = enitity.UpdatedBy;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The availability mode couldn't be removed.";
                this.logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                operationResult.Data = await (from availabilityModes in _medicalAppointmentContext.AvailabilityModes
                                              where availabilityModes.IsActive == true
                                              orderby availabilityModes.SAvailabilityModeID descending
                                              select new AvailabilityModesModel()
                                              {
                                                  SAvailabilityModeID = availabilityModes.SAvailabilityModeID,
                                                  AvailabilityMode = availabilityModes.AvailabilityMode,
                                                  UpdatedAt = availabilityModes.UpdatedAt,
                                                  UpdatedBy = availabilityModes.UpdatedBy,
                                              }).AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaining all the availability modes.";
                this.logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                operationResult.Data = await (from availabilityModes in _medicalAppointmentContext.AvailabilityModes
                                              where availabilityModes.IsActive == true && availabilityModes.SAvailabilityModeID == id
                                              orderby availabilityModes.SAvailabilityModeID descending
                                              select new AvailabilityModesModel()
                                              {
                                                  SAvailabilityModeID = availabilityModes.SAvailabilityModeID,
                                                  AvailabilityMode = availabilityModes.AvailabilityMode,
                                                  UpdatedAt = availabilityModes.UpdatedAt,
                                                  UpdatedBy = availabilityModes.UpdatedBy,
                                              }).AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaining all the availability modes.";
                this.logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }
    }
}