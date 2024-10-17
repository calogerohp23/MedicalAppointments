using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Appointment;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Appointments
{
    public class DoctorAvailabilityRepository : BaseRepository<DoctorAvailability>, IDoctorAvailabilityRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<DoctorAvailabilityRepository> logger;
        public DoctorAvailabilityRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<DoctorAvailabilityRepository> logger): base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }
        public async override Task<OperationResult> Save(DoctorAvailability entity)
        {
            OperationResult operationResult = new OperationResult();

            if(entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es nula";
                return operationResult;
            }
            if(entity.DoctorID == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "No ha seleccionado un doctor";
                return operationResult;
            }
            if (entity.StartTime == entity.EndTime) 
            {
                operationResult.Success = false;
                operationResult.Message = "El tiempo de inicio no puede ser igual que el tiempo final";
                return operationResult;
            }

            /* Fuese bueno agregar al Doctor Availability un update by, created by, updated at y created at*/
            try
            {
                operationResult = await base.Save(entity);
            }
            catch(Exception ex)
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

            if (entity == null) 
            {
                operationResult.Success= false;
                operationResult.Message = "La entidad es nula";
                return operationResult;
            }

            if (entity.StartTime == entity.EndTime)
            {
                operationResult.Success = false;
                operationResult.Message = "El tiempo de inicio no puede ser igual que el tiempo final";
                return operationResult;
            }

            try
            {
                DoctorAvailability? doctorAvailabilityToUpdate = await _medicalAppointmentContext.DoctorAvailability.FindAsync(entity.AvailabilityId);
                doctorAvailabilityToUpdate.DoctorID = entity.DoctorID;
                doctorAvailabilityToUpdate.AvailableDate = entity.AvailableDate;
                doctorAvailabilityToUpdate.StartTime = entity.StartTime;
                doctorAvailabilityToUpdate.EndTime = entity.EndTime;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Hubo un error actualizando la disponibilidad";
            }
            return operationResult;
        }
        public async override Task<OperationResult> 
    }
}
