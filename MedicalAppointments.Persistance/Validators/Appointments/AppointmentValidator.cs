using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.Appointments
{
    public class AppointmentValidator() : BaseValidator<Appointment>
    {

        public override OperationResult ValidateSave(Appointment entity)
        {
            OperationResult operationResult = new OperationResult();
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
                operationResult.Message = "The appointment date must be future dated.";
                return operationResult;
            }
            return operationResult;
        }

        public override OperationResult ValidateUpdate(int id, Appointment entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity.StatusID == 2)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment has been removed.";
                return operationResult;
            }
            if (entity.StatusID == 3)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment has been completed.";
                return operationResult;
            }
            if (entity.AppointmentDate.DayOfWeek == DayOfWeek.Saturday || entity.AppointmentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment can only be on business days.";
                return operationResult;
            }
            if (id != entity.AppointmentID)
            {

                operationResult.Success = false;
                operationResult.Message = "The selected appointment does not exist";
                return operationResult;
            }
            return operationResult;
        }

        public override OperationResult ValidateRemove(int id, Appointment entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity.StatusID == 2)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment has been removed.";
                return operationResult;
            }
            if (entity.StatusID == 3)
            {
                operationResult.Success = false;
                operationResult.Message = "The appointment has been completed.";
                return operationResult;
            }
            if (entity.AppointmentID != id)
            {
                operationResult.Success = false;
                operationResult.Message = "The selected appointment does not exist";
                return operationResult;
            }
            return operationResult;
        }
    }
}
