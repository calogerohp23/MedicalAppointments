using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.System;
using MedicalAppointments.Persistance.Models.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<StatusRepository> logger;

        public StatusRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<StatusRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Status entity)
        {
            OperationResult operationResult = new OperationResult();
            try 
            {
                await base.Save(entity);
            }
            catch(Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the status.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(Status entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                Status? statusToUpdate = await _medicalAppointmentContext.Status.FindAsync(entity.StatusId);
                statusToUpdate.StatusName = entity.StatusName;
               
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the status.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(Status enitity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                Status? statusToRemove = await _medicalAppointmentContext.Status.FindAsync(enitity.StatusId);
                statusToRemove.StatusName = enitity.StatusName;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the status.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();
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
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new OperationResult();
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
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the status.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}