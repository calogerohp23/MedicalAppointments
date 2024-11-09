using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.System
{
    public class StatusValidator : BaseValidator<Status>
    {
        public override OperationResult ValidateRemove(int id, Status entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }

        public override OperationResult ValidateSave(Status entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);

            return operationResult;
        }

        public override OperationResult ValidateUpdate(int id, Status entity)
        {
            OperationResult operationResult = new();
            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }
    }
}
