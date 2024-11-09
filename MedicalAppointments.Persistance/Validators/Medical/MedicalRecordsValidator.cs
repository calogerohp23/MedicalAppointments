using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.Medical
{
    public class MedicalRecordsValidator : BaseValidator<MedicalRecords>
    {
        public override OperationResult ValidateRemove(int id, MedicalRecords entity)
        {
            OperationResult operationResult = new();
            
            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }

        public override OperationResult ValidateSave(MedicalRecords entity)
        {
            OperationResult operationResult = new();
            
            base.ValidateNull(entity);

            return operationResult;
        }

        public override OperationResult ValidateUpdate(int id, MedicalRecords entity)
        {
            OperationResult operationResult = new();
            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }
    }
}
