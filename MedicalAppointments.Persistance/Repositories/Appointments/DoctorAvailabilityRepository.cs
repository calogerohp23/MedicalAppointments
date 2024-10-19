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
            OperationResult operationResult = new OperationResult();

            try
            {
                entity.IsActive = true;
                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Hubo un error guardando la disponiblidad del doctor";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }
        public async override Task<OperationResult> Update(DoctorAvailability entity)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                DoctorAvailability? doctorAvailabilityToUpdate = await _medicalAppointmentContext.DoctorAvailability.FindAsync(entity.AvailabilityId);
                doctorAvailabilityToUpdate.DoctorID = entity.DoctorID;
                doctorAvailabilityToUpdate.AvailableDate = entity.AvailableDate;
                doctorAvailabilityToUpdate.StartTime = entity.StartTime;
                doctorAvailabilityToUpdate.EndTime = entity.EndTime;
                doctorAvailabilityToUpdate.CreatedAt = entity.CreatedAt;
                doctorAvailabilityToUpdate.UpdatedAt = entity.UpdatedAt;
                doctorAvailabilityToUpdate.CreatedBy = entity.CreatedBy;
                doctorAvailabilityToUpdate.UpdatedBy = entity.UpdatedBy;

                await base.Update(doctorAvailabilityToUpdate);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error updating the doctor's availability.";
            }
            return operationResult;
        }
        public async override Task<OperationResult> Remove(DoctorAvailability entity)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                DoctorAvailability? doctorAvailabilityToRemove = await _medicalAppointmentContext.DoctorAvailability.FindAsync(entity.AvailabilityId);
                doctorAvailabilityToRemove.IsActive = false;
                doctorAvailabilityToRemove.UpdatedAt = entity.UpdatedAt;
                doctorAvailabilityToRemove.UpdatedBy = entity.UpdatedBy;

                await base.Update(doctorAvailabilityToRemove);
            }
            catch (Exception ex)
            {
                operationResult.Success= false;
                operationResult.Message = "There was an error removing the doctor's availability.";
                this.logger.LogError(operationResult.Message, ex);
            }

            return operationResult;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                operationResult.Data = await (from doctorAvailabity in _medicalAppointmentContext.DoctorAvailability
                                              join doctor in _medicalAppointmentContext.Doctors on doctorAvailabity.DoctorID equals doctor.DoctorID
                                              join users in _medicalAppointmentContext.Users on doctor.UserID equals users.UserID
                                              join specialties in _medicalAppointmentContext.Specialities on doctor.SpecialtyId equals specialties.SpecialtyID
                                              where doctorAvailabity.IsActive == true
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
            OperationResult operationResult = new OperationResult();

            try
            {
                operationResult.Data = await (from doctorAvailabity in _medicalAppointmentContext.DoctorAvailability
                                              join doctor in _medicalAppointmentContext.Doctors on doctorAvailabity.DoctorID equals doctor.DoctorID
                                              join users in _medicalAppointmentContext.Users on doctor.UserID equals users.UserID
                                              join specialties in _medicalAppointmentContext.Specialities on doctor.SpecialtyId equals specialties.SpecialtyID
                                              where doctorAvailabity.IsActive == true && doctorAvailabity.AvailabilityId == id
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
                operationResult.Message = "There was an error obtaining all the doctor's availabilities.";
                this.logger.LogError(operationResult.Message, ex);
            }

            return operationResult;
        }
    }
}
