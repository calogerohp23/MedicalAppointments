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
    public class RolesRepository : BaseRepository<Roles>, IRolesRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<RolesRepository> logger;
        private readonly RolesValidator _validator;

        public RolesRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<RolesRepository> logger, RolesValidator validator) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _validator = validator;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Roles entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateSave(entity);
            try
            {
                var result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Role couldn't be saved.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id, Roles entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateUpdate(id, entity);
            try
            {
                Roles? rolesToUpdate = await _medicalAppointmentContext.Roles.FindAsync(id);
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
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(int id, Roles entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateRemove(id, entity);
            try
            {
                Roles? rolesToRemove = await _medicalAppointmentContext.Roles.FindAsync(id);
                rolesToRemove.RoleName = entity.RoleName;
                rolesToRemove.UpdatedAt = entity.UpdatedAt;
                rolesToRemove.UpdatedBy = entity.UpdatedBy;
                rolesToRemove.IsActive = entity.IsActive;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Role couldn't be saved.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from roles in _medicalAppointmentContext.Roles
                                              orderby roles.RoleID descending
                                              select new RolesModel()
                                              {
                                                  RoleID = roles.RoleID,
                                                  RoleName = roles.RoleName,
                                                  CreatedAt = roles.CreatedAt,
                                                  CreatedBy = roles.CreatedBy,
                                                  UpdatedAt = roles.UpdatedAt,
                                                  UpdatedBy = roles.UpdatedBy,
                                                  IsActive = roles.IsActive,
                                              }).AsNoTracking().
                                              ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Role couldn't be saved.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from roles in _medicalAppointmentContext.Roles
                                              where roles.RoleID == id
                                              orderby roles.RoleID descending
                                              select new RolesModel()
                                              {
                                                  RoleID = roles.RoleID,
                                                  RoleName = roles.RoleName,
                                                  CreatedAt = roles.CreatedAt,
                                                  CreatedBy = roles.CreatedBy,
                                                  UpdatedAt = roles.UpdatedAt,
                                                  UpdatedBy = roles.UpdatedBy,
                                                  IsActive = roles.IsActive,
                                              }).AsNoTracking().
                              ToListAsync();
                operationResult.Data = _validator.ValidateNullData(operationResult.Data);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Role couldn't be saved.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}