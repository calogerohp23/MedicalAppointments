﻿using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Appointment;
using MedicalAppointments.Persistance.Models;
using MedicalAppointments.Persistance.Models.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Appointments
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<AppointmentRepository> logger;
        public AppointmentRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<AppointmentRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Appointment entity)
        {
            OperationResult operationResult = new();
            // se repite
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null.";
                return operationResult;
            }
            if (entity.PatientID == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "A patient hasn't been selected.";
                return operationResult;
            }
            if (entity.DoctorID == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "A doctor hasn't been selected.";
                return operationResult;
            }
            if (entity.AppointmentDate < DateTime.UtcNow)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment date must be different.";
                return operationResult;
            }

            if (await base.Exists(appointment => appointment.AppointmentID == entity.AppointmentID
                      && appointment.PatientID == entity.PatientID && appointment.DoctorID == entity.DoctorID))
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment already exist.";
                return operationResult;
            }

            // se repite - solo para appointemnts
            if (entity.AppointmentDate.DayOfWeek == DayOfWeek.Saturday || entity.AppointmentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment can only be on business days.";
                return operationResult;
            }
            try
            {

                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "An error ocurred while saving the appointment.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(Appointment entity)
        {
            OperationResult operationResult = new();

            // se repite
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null.";
                return operationResult;
            }
            // se repite - solo para appointemnts
            if (entity.StatusID == 2)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment has been removed.";
                return operationResult;
            }
            // se repite - solo para appointemnts
            if (entity.StatusID == 3)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment has been completed.";
                return operationResult;
            }
            // se repite - solo para appointemnts
            if (entity.AppointmentDate.DayOfWeek == DayOfWeek.Saturday || entity.AppointmentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment can only be on business days.";
                return operationResult;
            }
            try
            {
                Appointment? appointmentToUpdate = await _medicalAppointmentContext.Appointments.FindAsync(entity.AppointmentID);
                appointmentToUpdate.PatientID = entity.PatientID;
                appointmentToUpdate.DoctorID = entity.DoctorID;
                appointmentToUpdate.AppointmentDate = entity.AppointmentDate;
                appointmentToUpdate.StatusID = entity.StatusID;
                appointmentToUpdate.UpdatedAt = entity.UpdatedAt;
                appointmentToUpdate.UpdatedBy = entity.UpdatedBy;
                appointmentToUpdate.IsActive = entity.IsActive;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "An error ocurred while updating the appointment";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(Appointment entity)
        {
            OperationResult operationResult = new();
            // se repite
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null.";
                return operationResult;
            }
            // se repite - solo para appointemnts
            if (entity.StatusID == 2)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment has been removed.";
                return operationResult;
            }
            // se repite - solo para appointemnts
            if (entity.StatusID == 3)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment has been completed.";
                return operationResult;
            }
            try
            {
                Appointment? appointmentToRemove = await _medicalAppointmentContext.Appointments.FindAsync(entity.AppointmentID);
                appointmentToRemove.StatusID = 2;
                appointmentToRemove.UpdatedAt = entity.UpdatedAt;
                appointmentToRemove.UpdatedBy = entity.UpdatedBy;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "An error ocurred while removing the appointment.";
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
                operationResult.Message = "Error obteniendo las reservaciones";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new();
            if (id == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "The ID is null";
                return operationResult;
            }
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
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo las reservaciones";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}
