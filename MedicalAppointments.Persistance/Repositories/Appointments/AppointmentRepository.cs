using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Appointment;
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
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es nula";
                return operationResult;
            }

            if (entity.AppointmentDate <= DateTime.UtcNow)
            {
                operationResult.Success = false;
                operationResult.Message = "El appointment esta en una fecha pasada";
                return operationResult;
            }

            if (entity.PatientID == 0) {
                operationResult.Success = false;
                operationResult.Message = "No ha seleccionado un paciente";
                return operationResult;
            }
            if (entity.DoctorID == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "No ha seleccionado un doctor";
                return operationResult;
            }

            try
            {
                entity.StatusID = 1;
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
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
            OperationResult operationResult = new OperationResult();


            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es nula";
                return operationResult;
            }
            if (entity.StatusID != 1)
            {
                operationResult.Success = false;
                operationResult.Message = "La reservacion ya no puede ser actualizada";
                return operationResult;
            }
            try
            {
                Appointment? appointmentToUpdate = await _medicalAppointmentContext.Appointments.FindAsync(entity.AppointmentID);
                appointmentToUpdate.DoctorID = entity.AppointmentID;
                appointmentToUpdate.AppointmentDate = entity.AppointmentDate;
                appointmentToUpdate.StatusID = entity.StatusID;
                appointmentToUpdate.UpdatedAt = entity.UpdatedAt;
                appointmentToUpdate.UpdatedBy = entity.UpdatedBy;

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
            OperationResult operationResult = new OperationResult();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida";
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
        public async override Task<OperationResult> GetAll(Appointment entity)
        {
            OperationResult operationResult = new OperationResult();
            if ()
            {

            }
            if ()
            {

            }
            try
            {
                operationResult.Data = await(from appointment in _medicalAppointmentContext.Appointments
                                          
            }
            catch (Exception ex) 
            { 
            
            }
            return operationResult;
        }
    }
}
