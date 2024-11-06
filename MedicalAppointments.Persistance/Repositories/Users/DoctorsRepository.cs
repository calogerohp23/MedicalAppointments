using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Users;
using MedicalAppointments.Persistance.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Users
{
    public class DoctorsRepository : BaseRepository<Doctors>, IDoctorsRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<DoctorsRepository> logger;

        public DoctorsRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<DoctorsRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Doctors entity)
        {
            OperationResult operationResult = new();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null.";
                return operationResult;
            }
            if (entity.LicenseExpirationDate < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                operationResult.Success = false;
                operationResult.Message = "The Doctor's licence is already expired";
                return operationResult;
            }
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id, Doctors entity)
        {
            OperationResult operationResult = new();
            try
            {
                Doctors? doctorsToUpdate = await _medicalAppointmentContext.Doctors.FindAsync(id);
                doctorsToUpdate.SpecialtyId = entity.SpecialtyId;
                doctorsToUpdate.UserID = entity.UserID;
                doctorsToUpdate.LicenseNumber = entity.LicenseNumber;
                doctorsToUpdate.PhoneNumber = entity.PhoneNumber;
                doctorsToUpdate.YearsOfExperience = entity.YearsOfExperience;
                doctorsToUpdate.Education = entity.Education;
                doctorsToUpdate.Bio = entity.Bio;
                doctorsToUpdate.ConsultationFee = entity.ConsultationFee;
                doctorsToUpdate.ClinicAddress = entity.ClinicAddress;
                doctorsToUpdate.AvailabilityModeId = entity.AvailabilityModeId;
                doctorsToUpdate.LicenseExpirationDate = entity.LicenseExpirationDate;
                doctorsToUpdate.UpdatedAt = entity.UpdatedAt;
                doctorsToUpdate.UpdatedBy = entity.UpdatedBy;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(int id, Doctors entity)
        {
            OperationResult operationResult = new();
            try
            {
                Doctors? doctorsToRemove = await _medicalAppointmentContext.Doctors.FindAsync(id);
                doctorsToRemove.IsActive = false;
                doctorsToRemove.UpdatedAt = entity.UpdatedAt;
                doctorsToRemove.UpdatedBy = entity.UpdatedBy;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from doctors in _medicalAppointmentContext.Doctors
                                              join specialties in _medicalAppointmentContext.Specialities on doctors.SpecialtyId equals specialties.SpecialtyID
                                              join availability in _medicalAppointmentContext.AvailabilityModes on doctors.AvailabilityModeId equals availability.SAvailabilityModeID
                                              join users in _medicalAppointmentContext.Users on doctors.UserID equals users.UserID
                                              where doctors.IsActive == true
                                              select new DoctorsSpecialityAvailabilityModeUserModel()
                                              {
                                                  DoctorID = doctors.DoctorID,
                                                  FullName = users.FirstName + " " + users.LastName,
                                                  SpecialtyName = specialties.SpecialtyName,
                                                  LicenceNumber = doctors.LicenseNumber,
                                                  PhoneNumber = doctors.PhoneNumber,
                                                  YearsOfExperience = doctors.YearsOfExperience,
                                                  Education = doctors.Education,
                                                  Bio = doctors.Bio,
                                                  ConsultationFee = doctors.ConsultationFee,
                                                  ClinicAddress = doctors.ClinicAddress,
                                                  AvailabilityMode = availability.AvailabilityMode,
                                                  LicenceExpirationDate = doctors.LicenseExpirationDate,
                                                  CreatedAt = doctors.CreatedAt,
                                                  CreatedBy = doctors.CreatedBy,
                                                  UpdatedAt = doctors.UpdatedAt,
                                                  UpdatedBy = doctors.UpdatedBy,
                                              }).AsNoTracking()
                                              .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from doctors in _medicalAppointmentContext.Doctors
                                              join specialties in _medicalAppointmentContext.Specialities on doctors.SpecialtyId equals specialties.SpecialtyID
                                              join availability in _medicalAppointmentContext.AvailabilityModes on doctors.AvailabilityModeId equals availability.SAvailabilityModeID
                                              join users in _medicalAppointmentContext.Users on doctors.UserID equals users.UserID
                                              where doctors.IsActive == true && doctors.DoctorID == id
                                              select new DoctorsSpecialityAvailabilityModeUserModel()
                                              {
                                                  DoctorID = doctors.DoctorID,
                                                  FullName = users.FirstName + " " + users.LastName,
                                                  SpecialtyName = specialties.SpecialtyName,
                                                  LicenceNumber = doctors.LicenseNumber,
                                                  PhoneNumber = doctors.PhoneNumber,
                                                  YearsOfExperience = doctors.YearsOfExperience,
                                                  Education = doctors.Education,
                                                  Bio = doctors.Bio,
                                                  ConsultationFee = doctors.ConsultationFee,
                                                  ClinicAddress = doctors.ClinicAddress,
                                                  AvailabilityMode = availability.AvailabilityMode,
                                                  LicenceExpirationDate = doctors.LicenseExpirationDate,
                                                  CreatedAt = doctors.CreatedAt,
                                                  CreatedBy = doctors.CreatedBy,
                                                  UpdatedAt = doctors.UpdatedAt,
                                                  UpdatedBy = doctors.UpdatedBy,
                                              }).AsNoTracking()
                              .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}