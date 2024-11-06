using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Appointment;
using MedicalAppointments.Persistance.Models.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Appointments
{
    public class DoctorAvailabilityRepository : BaseRepository<DoctorAvailability>, IDoctorAvailabilityRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<DoctorAvailabilityRepository> logger;
        public DoctorAvailabilityRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<DoctorAvailabilityRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }
        public async override Task<OperationResult> Save(DoctorAvailability entity)
        {
            OperationResult operationResult = new();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null.";
                return operationResult;

            }
            if (entity.DoctorID == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "The doctor hasn't been selected.";
            }
            if (entity.StartTime == entity.EndTime)
            {
                operationResult.Success = false;
                operationResult.Message = "The start time cannot be the same as the end time.";
                return operationResult;
            }
            if (entity.StartTime >= entity.EndTime)
            {
                operationResult.Success = false;
                operationResult.Message = "The start time cannot be after the end time.";
                return operationResult;
            }
            if (await base.Exists(doctorAvailability => doctorAvailability.DoctorID == entity.DoctorID))
            {
                operationResult.Success = false;
                operationResult.Message = "The doctor availability time is already registered.";
                return operationResult;
            }
            try
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                operationResult = await base.Save(entity);

                operationResult.Success = true;
                operationResult.Message = "The docotor's availability has been was saved!";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor's availability.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }
        public async override Task<OperationResult> Update(int id, DoctorAvailability entity)
        {
            OperationResult operationResult = new();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null.";
                return operationResult;

            }
            if (entity.DoctorID == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "The doctor hasn't been selected.";
            }
            if (entity.StartTime == entity.EndTime)
            {
                operationResult.Success = false;
                operationResult.Message = "The start time cannot be the same as the end time.";
                return operationResult;
            }
            if (entity.StartTime >= entity.EndTime)
            {
                operationResult.Success = false;
                operationResult.Message = "The start time cannot be after the end time.";
                return operationResult;
            }
            try
            {
                DoctorAvailability? doctorAvailabilityToUpdate = await _medicalAppointmentContext.DoctorAvailability.FindAsync(id);
                doctorAvailabilityToUpdate.DoctorID = entity.DoctorID;
                doctorAvailabilityToUpdate.AvailableDate = entity.AvailableDate;
                doctorAvailabilityToUpdate.StartTime = entity.StartTime;
                doctorAvailabilityToUpdate.EndTime = entity.EndTime;
                doctorAvailabilityToUpdate.UpdatedAt = DateTime.Now;
                doctorAvailabilityToUpdate.UpdatedBy = entity.UpdatedBy;

                await _medicalAppointmentContext.SaveChangesAsync();
                operationResult.Success = true;
                operationResult.Message = "The doctor's availability was updated succesully!";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error updating the doctor's availability.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        public async override Task<OperationResult> Remove(int id, DoctorAvailability entity)
        {
            OperationResult operationResult = new();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null.";
                return operationResult;

            }
            try
            {
                DoctorAvailability? doctorAvailabilityToRemove = await _medicalAppointmentContext.DoctorAvailability.FindAsync(id);
                doctorAvailabilityToRemove.IsActive = false;
                doctorAvailabilityToRemove.UpdatedAt = DateTime.Now;
                doctorAvailabilityToRemove.UpdatedBy = entity.UpdatedBy;

                await _medicalAppointmentContext.SaveChangesAsync();
                operationResult.Success = true;
                operationResult.Message = "The doctor's availability was disabled succesfully!";

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error removing the doctor's availability.";
                this.logger.LogError(operationResult.Message, ex);
            }

            return operationResult;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();

            try
            {
                operationResult.Data = await (from doctorAvailabity in _medicalAppointmentContext.DoctorAvailability
                                              join doctor in _medicalAppointmentContext.Doctors on doctorAvailabity.DoctorID equals doctor.DoctorID
                                              join users in _medicalAppointmentContext.Users on doctor.UserID equals users.UserID
                                              join specialties in _medicalAppointmentContext.Specialities on doctor.SpecialtyId equals specialties.SpecialtyID
                                              select new DoctorAvailabilityDoctorSpecialtyUsersModel()
                                              {
                                                  AvailabilityID = doctorAvailabity.AvailabilityId,
                                                  DoctorId = doctor.DoctorID,
                                                  FullName = users.FirstName + " " + users.LastName,
                                                  Specialty = specialties.SpecialtyName,
                                                  AvailableDate = doctorAvailabity.AvailableDate,
                                                  StartTime = doctorAvailabity.StartTime,
                                                  EndTime = doctorAvailabity.EndTime,
                                                  CreatedAt = doctorAvailabity.CreatedAt,
                                                  CreatedBy = doctorAvailabity.CreatedBy
                                              }).AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaining all the doctor's availability.";
                this.logger.LogError(operationResult.Message, ex);
            }

            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new();

            try
            {
                operationResult.Data = await (from doctorAvailabity in _medicalAppointmentContext.DoctorAvailability
                                              join doctor in _medicalAppointmentContext.Doctors on doctorAvailabity.DoctorID equals doctor.DoctorID
                                              join users in _medicalAppointmentContext.Users on doctor.UserID equals users.UserID
                                              join specialties in _medicalAppointmentContext.Specialities on doctor.SpecialtyId equals specialties.SpecialtyID
                                              where doctorAvailabity.AvailabilityId == id
                                              select new DoctorAvailabilityDoctorSpecialtyUsersModel()
                                              {
                                                  AvailabilityID = doctorAvailabity.AvailabilityId,
                                                  DoctorId = doctor.DoctorID,
                                                  FullName = users.FirstName + " " + users.LastName,
                                                  Specialty = specialties.SpecialtyName,
                                                  AvailableDate = doctorAvailabity.AvailableDate,
                                                  StartTime = doctorAvailabity.StartTime,
                                                  EndTime = doctorAvailabity.EndTime,
                                                  CreatedAt = doctorAvailabity.CreatedAt,
                                                  CreatedBy = doctorAvailabity.CreatedBy
                                              }).AsNoTracking()
                                            .ToListAsync();
                if (operationResult.Data == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "The ID does not exist on the database.";
                }

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaining all the doctor's availabilities.";
                this.logger.LogError(operationResult.Message, ex);
            }

            return operationResult;
        }
    }
}
