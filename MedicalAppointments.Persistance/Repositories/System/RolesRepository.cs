﻿using MedicalAppointments.Domain.Entities.System;
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
            try
            {
                await base.Save(entity);
            }
            catch(Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Role couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(Roles entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                Roles? rolesToUpdate = await _medicalAppointmentContext.Roles.FindAsync(entity.RoleID);
                rolesToUpdate.RoleName = entity.RoleName;
                rolesToUpdate.CreatedAt = entity.CreatedAt;
                rolesToUpdate.CreatedBy = entity.CreatedBy;
                rolesToUpdate.UpdatedAt = entity.UpdatedAt;
                rolesToUpdate.UpdatedBy = entity.UpdatedBy;
                rolesToUpdate.IsActive = entity.IsActive;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Role couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(Roles entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                Roles? rolesToRemove = await _medicalAppointmentContext.Roles.FindAsync(entity.RoleID);
                rolesToRemove.RoleName = entity.RoleName;
                rolesToRemove.UpdatedAt = entity.UpdatedAt;
                rolesToRemove.UpdatedBy = entity.UpdatedBy;
                rolesToRemove.IsActive = entity.IsActive;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Role couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();
            try
            {

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Role couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Role couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}