using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Appointment;
using MedicalAppointments.Persistance.Models.Appointments;
using MedicalAppointments.Persistance.Validators.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Appointments
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<AppointmentRepository> logger;
        private readonly AppointmentValidator _validator;
        public AppointmentRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<AppointmentRepository> logger, AppointmentValidator appointmentValidator) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _validator = appointmentValidator;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Appointment entity)
        {
            OperationResult operationResult = new();

            _validator.ValidateNull(entity);
            _validator.ValidateSave(entity);

            if (await base.Exists(appointment => appointment.AppointmentID == entity.AppointmentID
                      && appointment.PatientID == entity.PatientID && appointment.DoctorID == entity.DoctorID))
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment already exist.";
                return operationResult;
            }
            try
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "An error occurred while saving the appointment.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id, Appointment entity)
        {
            OperationResult operationResult = new();

            _validator.ValidateNull(entity);
            _validator.ValidateID(id);
            _validator.ValidateUpdate(id, entity);

            try
            {
                Appointment? appointmentToUpdate = await _medicalAppointmentContext.Appointments.FindAsync(id);
                appointmentToUpdate.PatientID = entity.PatientID;
                appointmentToUpdate.DoctorID = entity.DoctorID;
                appointmentToUpdate.AppointmentDate = entity.AppointmentDate;
                appointmentToUpdate.UpdatedAt = DateTime.Now;
                appointmentToUpdate.UpdatedBy = entity.UpdatedBy;

                await _medicalAppointmentContext.SaveChangesAsync();
                operationResult.Success = true;
                operationResult.Message = "The appointment was updated succesfully";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "An error ocurred while updating the appointment";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(int id, Appointment entity)
        {
            OperationResult operationResult = new();

            _validator.ValidateNull(entity);
            _validator.ValidateID(id);
            _validator.ValidateRemove(id, entity);
            try
            {
                Appointment? appointmentToRemove = await _medicalAppointmentContext.Appointments.FindAsync(id);
                appointmentToRemove.StatusID = 2;
                appointmentToRemove.UpdatedAt = DateTime.Now;
                appointmentToRemove.UpdatedBy = entity.UpdatedBy;

                await _medicalAppointmentContext.SaveChangesAsync();
                operationResult.Success = true;
                operationResult.Message = "The appointment was disabled successfully!";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "An error occurred while removing the appointment.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from appointment in _medicalAppointmentContext.Appointments
                                              join patients in _medicalAppointmentContext.Patients on appointment.PatientID equals patients.PatientID
                                              join doctors in _medicalAppointmentContext.Doctors on appointment.DoctorID equals doctors.DoctorID
                                              join status in _medicalAppointmentContext.Status on appointment.StatusID equals status.StatusId
                                              join patientUsers in _medicalAppointmentContext.Users on patients.UserID equals patientUsers.UserID
                                              join doctorUsers in _medicalAppointmentContext.Users on doctors.UserID equals doctorUsers.UserID
                                              orderby appointment.AppointmentDate descending
                                              select new AppointmentPatientDoctorStatusModel()
                                              {
                                                  AppointmentID = appointment.AppointmentID,
                                                  Patient = patientUsers.FirstName + " " + patientUsers.LastName,
                                                  Doctor = doctorUsers.FirstName + " " + doctorUsers.LastName,
                                                  Status = status.StatusName,
                                                  AppointmentDate = appointment.AppointmentDate,
                                                  CreatedAt = appointment.CreatedAt,
                                                  UpdatedAt = appointment.UpdatedAt,
                                                  CreatedBy = appointment.CreatedBy,
                                                  UpdatedBy = appointment.UpdatedBy
                                              }).AsNoTracking()
                                            .ToListAsync();

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "An error occurred while obtaining the appointments registry.";
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
                operationResult.Data = await (from appointment in _medicalAppointmentContext.Appointments
                                              join patients in _medicalAppointmentContext.Patients on appointment.PatientID equals patients.PatientID
                                              join doctors in _medicalAppointmentContext.Doctors on appointment.DoctorID equals doctors.DoctorID
                                              join status in _medicalAppointmentContext.Status on appointment.StatusID equals status.StatusId
                                              join patientUsers in _medicalAppointmentContext.Users on patients.UserID equals patientUsers.UserID
                                              join doctorUsers in _medicalAppointmentContext.Users on doctors.UserID equals doctorUsers.UserID
                                              where appointment.AppointmentID == id
                                              orderby appointment.AppointmentDate descending
                                              select new AppointmentPatientDoctorStatusModel
                                              {
                                                  AppointmentID = appointment.AppointmentID,
                                                  Patient = patientUsers.FirstName + " " + patientUsers.LastName,
                                                  Doctor = doctorUsers.FirstName + " " + doctorUsers.LastName,
                                                  Status = status.StatusName,
                                                  AppointmentDate = appointment.AppointmentDate,
                                                  CreatedAt = appointment.CreatedAt,
                                                  UpdatedAt = appointment.UpdatedAt,
                                                  CreatedBy = appointment.CreatedBy,
                                                  UpdatedBy = appointment.UpdatedBy
                                              }).FirstOrDefaultAsync();
                
                operationResult.Data = _validator.ValidateNullData(operationResult.Data);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "An error occurred while obtaining the appointments registry.";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}
