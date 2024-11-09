using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.Medical
{
    public class AvailabilityModesValidator : BaseValidator<AvailabilityModes>
    {
        public override OperationResult ValidateRemove(int id, AvailabilityModes entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);
            base.ValidateID(id);
            return operationResult;
        }

        public override OperationResult ValidateSave(AvailabilityModes entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);

            return operationResult;

        }

        public override OperationResult ValidateUpdate(int id, AvailabilityModes entity)
        {
            OperationResult operationResult = new();
            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }
    }
}
