using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.Appointments
{
    public class DoctorAvailabilityValidator : BaseValidator<DoctorAvailability>
    {
        public override OperationResult ValidateRemove(int id, DoctorAvailability entity)
        {
            OperationResult operationResult = new();

            if (id != entity.AvailabilityId)
            {
                operationResult.Success = false;
                operationResult.Message = "The selected availability does not exist.";
                return operationResult;
            }
            return operationResult;
        }

        public override OperationResult ValidateSave(DoctorAvailability entity)
        {
            OperationResult operationResult = new();

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

            return operationResult;
        }

        public override OperationResult ValidateUpdate(int id, DoctorAvailability entity)
        {
            OperationResult operationResult = new();

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
            if (id != entity.AvailabilityId)
            {
                operationResult.Success = false;
                operationResult.Message = "The selected availability does not exist.";
                return operationResult;
            }
            return operationResult;
        }
    }
}
