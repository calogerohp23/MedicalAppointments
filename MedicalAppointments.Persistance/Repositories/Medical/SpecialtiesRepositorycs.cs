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
    public class SpecialtiesRepositroy : BaseRepository<Specialities>, ISpecialtiesRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<SpecialtiesRepositroy> logger;

        public SpecialtiesRepositroy(MedicalAppointmentContext medicalAppointmentContext, ILogger<SpecialtiesRepositroy> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Specialities entity)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The specialty couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(Specialities entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                Specialities? specialtiesToUpdate = await _medicalAppointmentContext.Specialities.FindAsync(entity.SpecialtyID);
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
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(Specialities entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                Specialities? specialtiesToRemove = await _medicalAppointmentContext.Specialities.FindAsync(entity.SpecialtyID);
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
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                operationResult.Data = await (from specialties in _medicalAppointmentContext.Specialities
                                              where specialties.IsActive == true
                                              orderby specialties.SpecialtyID descending
                                              select new SpecialtiesModel()
                                              {
                                                  SpecialtyID = specialties.SpecialtyID,
                                                  SpecialtyName = specialties.SpecialtyName,
                                                  CreatedAt = specialties.CreatedAt,
                                                  CreatedBy = specialties.CreatedBy,
                                              }).AsNoTracking()
                                             .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The specialty couldn't be obtained.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                operationResult.Data = await (from specialties in _medicalAppointmentContext.Specialities
                                              where specialties.IsActive == true
                                              orderby specialties.SpecialtyID descending
                                              where specialties.SpecialtyID == id
                                              select new SpecialtiesModel()
                                              {
                                                  SpecialtyID = specialties.SpecialtyID,
                                                  SpecialtyName = specialties.SpecialtyName,
                                                  CreatedAt = specialties.CreatedAt,
                                                  CreatedBy = specialties.CreatedBy,
                                              }).AsNoTracking()
                                             .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The specialty couldn't be obtained.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}