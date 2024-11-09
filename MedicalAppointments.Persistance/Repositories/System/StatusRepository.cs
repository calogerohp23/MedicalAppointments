using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.System;
using MedicalAppointments.Persistance.Models.System;
using MedicalAppointments.Persistance.Validators.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.System
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<StatusRepository> logger;
        private readonly StatusValidator _validator;

        public StatusRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<StatusRepository> logger, StatusValidator validator) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _validator = validator;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Status entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateSave(entity);
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the status.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id, Status entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateUpdate(id, entity);
            try
            {
                Status? statusToUpdate = await _medicalAppointmentContext.Status.FindAsync(id);
                statusToUpdate.StatusName = entity.StatusName;

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the status.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(int id, Status entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateRemove(id, entity);
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null.";
                return operationResult;
            }
            try
            {
                Status? statusToRemove = await _medicalAppointmentContext.Status.FindAsync(id);
                statusToRemove.StatusName = entity.StatusName;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the status.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from status in _medicalAppointmentContext.Status
                                              select new StatusModel()
                                              {
                                                  StatusID = status.StatusId,
                                                  StatusName = status.StatusName,
                                              }).AsNoTracking()
                                        .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the status.";
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
                operationResult.Data = await (from status in _medicalAppointmentContext.Status
                                              where status.StatusId == id
                                              select new StatusModel()
                                              {
                                                  StatusID = status.StatusId,
                                                  StatusName = status.StatusName,
                                              }).AsNoTracking()
                        .ToListAsync();
                operationResult.Data = _validator.ValidateNullData(operationResult.Data);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the status.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}