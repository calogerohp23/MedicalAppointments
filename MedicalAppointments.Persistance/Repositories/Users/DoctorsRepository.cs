using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Users;
using MedicalAppointments.Persistance.Models.Users;
using MedicalAppointments.Persistance.Validators.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Users
{
    public class DoctorsRepository : BaseRepository<Doctors>, IDoctorsRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<DoctorsRepository> logger;
        private readonly DoctorsValidator _validator;

        public DoctorsRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<DoctorsRepository> logger, DoctorsValidator validator) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _validator = validator;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Doctors entity)
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
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id, Doctors entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateUpdate(id, entity);
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
                doctorsToUpdate.UpdatedAt = DateTime.Now;
                doctorsToUpdate.UpdatedBy = entity.UpdatedBy;

                await _medicalAppointmentContext.SaveChangesAsync();
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
            _validator.ValidateRemove(id, entity);
            try
            {
                Doctors? doctorsToRemove = await _medicalAppointmentContext.Doctors.FindAsync(id);
                doctorsToRemove.IsActive = false;
                doctorsToRemove.UpdatedAt = DateTime.Now;
                doctorsToRemove.UpdatedBy = entity.UpdatedBy;

                await _medicalAppointmentContext.SaveChangesAsync();
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
                operationResult.Data = _validator.ValidateNullData(operationResult.Data);
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