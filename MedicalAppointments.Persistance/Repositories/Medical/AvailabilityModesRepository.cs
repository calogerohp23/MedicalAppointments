using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Medical;
using MedicalAppointments.Persistance.Models.Medical;
using MedicalAppointments.Persistance.Validators.Medical;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Medical
{
    public class AvailabilityModesRepository : BaseRepository<AvailabilityModes>, IAvailabilityModesRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<AvailabilityModesRepository> logger;
        private readonly AvailabilityModesValidator _validator;

        public AvailabilityModesRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<AvailabilityModesRepository> logger, AvailabilityModesValidator validator) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _validator = validator;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(AvailabilityModes entity)
        {
            OperationResult operationResult = new();

            _validator.ValidateSave(entity);

            try
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;

                await base.Save(entity);

                operationResult.Success = true;
                operationResult.Message = "The availability mode was successfully saved!";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The availability mode couldn't be saved.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id, AvailabilityModes entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateUpdate(id, entity);
            try
            {
                AvailabilityModes? availabilityModesToUpdate = await _medicalAppointmentContext.AvailabilityModes.FindAsync(id);
                availabilityModesToUpdate.AvailabilityMode = entity.AvailabilityMode;
                availabilityModesToUpdate.UpdatedAt = DateTime.Now;
                availabilityModesToUpdate.UpdatedBy = entity.UpdatedBy;

                await _medicalAppointmentContext.SaveChangesAsync();

                operationResult.Success = true;
                operationResult.Message = "The availability mode was successfully updated!";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The availability mode couldn't be saved.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(int id, AvailabilityModes entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateRemove(id, entity);
            try
            {
                AvailabilityModes? availabilityModesToRemove = await _medicalAppointmentContext.AvailabilityModes.FindAsync(id);
                availabilityModesToRemove.IsActive = false;
                availabilityModesToRemove.UpdatedAt = DateTime.Now;
                availabilityModesToRemove.UpdatedBy = entity.UpdatedBy;

                await _medicalAppointmentContext.SaveChangesAsync();
                operationResult.Success = true;
                operationResult.Message = "The availability Mode was successfully disabled!";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;

                operationResult.Message = "The availability mode couldn't be removed.";
                logger.LogError(operationResult.Message, ex.ToString());
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
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new();
            _validator.ValidateID(id);
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
                operationResult.Data = _validator.ValidateNullData(operationResult.Data);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaining all the availability modes.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}