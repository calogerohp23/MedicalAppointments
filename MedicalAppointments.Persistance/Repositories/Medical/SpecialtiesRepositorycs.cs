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
    public class SpecialtiesRepositroy : BaseRepository<Specialties>, ISpecialtiesRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<SpecialtiesRepositroy> logger;
        private readonly SpecialtiesValidator _validator;

        public SpecialtiesRepositroy(MedicalAppointmentContext medicalAppointmentContext, ILogger<SpecialtiesRepositroy> logger, SpecialtiesValidator validator) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _validator = validator;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Specialties entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateSave(entity);
            try
            {
                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The specialty couldn't be saved.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id, Specialties entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateUpdate(id, entity);
            try
            {
                Specialties? specialtiesToUpdate = await _medicalAppointmentContext.Specialities.FindAsync(id);
                specialtiesToUpdate.SpecialtyName = entity.SpecialtyName;
                specialtiesToUpdate.CreatedAt = entity.CreatedAt;
                specialtiesToUpdate.CreatedBy = entity.CreatedBy;
                specialtiesToUpdate.UpdatedAt = entity.UpdatedAt;
                specialtiesToUpdate.UpdatedBy = entity.UpdatedBy;
                specialtiesToUpdate.IsActive = entity.IsActive;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The specialty couldn't be updated.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(int id, Specialties entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateRemove(id, entity);
            try
            {
                Specialties? specialtiesToRemove = await _medicalAppointmentContext.Specialities.FindAsync(id);
                specialtiesToRemove.SpecialtyName = entity.SpecialtyName;
                specialtiesToRemove.CreatedAt = entity.CreatedAt;
                specialtiesToRemove.CreatedBy = entity.CreatedBy;
                specialtiesToRemove.UpdatedAt = entity.UpdatedAt;
                specialtiesToRemove.UpdatedBy = entity.UpdatedBy;
                specialtiesToRemove.IsActive = entity.IsActive;

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The specialty couldn't be removed.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from specialties in _medicalAppointmentContext.Specialities
                                              join createdUser in _medicalAppointmentContext.Users on specialties.CreatedBy equals createdUser.UserID
                                              join updatedUser in _medicalAppointmentContext.Users on specialties.UpdatedBy equals updatedUser.UserID
                                              where specialties.IsActive == true
                                              orderby specialties.SpecialtyID descending
                                              select new SpecialtiesModel()
                                              {
                                                  SpecialtyID = specialties.SpecialtyID,
                                                  SpecialtyName = specialties.SpecialtyName,
                                                  CreatedAt = specialties.CreatedAt,
                                                  UpdatedAt = specialties.UpdatedAt,
                                                  CreatedBy = createdUser.FirstName + " " + createdUser.LastName,
                                                  UpdatedBy = updatedUser.FirstName + " " + updatedUser.LastName,
                                                  IsActive = specialties.IsActive,
                                              }).AsNoTracking()
                                             .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The specialty couldn't be obtained.";
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
                operationResult.Data = await (from specialties in _medicalAppointmentContext.Specialities
                                              join createdUser in _medicalAppointmentContext.Users on specialties.CreatedBy equals createdUser.UserID
                                              join updatedUser in _medicalAppointmentContext.Users on specialties.UpdatedBy equals updatedUser.UserID
                                              where specialties.IsActive == true
                                              orderby specialties.SpecialtyID descending
                                              where specialties.SpecialtyID == id
                                              select new SpecialtiesModel()
                                              {
                                                  SpecialtyID = specialties.SpecialtyID,
                                                  SpecialtyName = specialties.SpecialtyName,
                                                  CreatedAt = specialties.CreatedAt,
                                                  UpdatedAt = specialties.UpdatedAt,
                                                  CreatedBy = createdUser.FirstName + " " + createdUser.LastName,
                                                  UpdatedBy = updatedUser.FirstName + " " + updatedUser.LastName,
                                                  IsActive = specialties.IsActive,

                                              }).AsNoTracking()
                                             .ToListAsync();
               operationResult.Data = _validator.ValidateNullData(operationResult.Data);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The specialty couldn't be obtained.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}