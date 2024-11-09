using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.System
{
    public class NotificationsValidator : BaseValidator<Notifications>
    {
        public override OperationResult ValidateRemove(int id, Notifications entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }

        public override OperationResult ValidateSave(Notifications entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);

            return operationResult;
        }

        public override OperationResult ValidateUpdate(int id, Notifications entity)
        {
            OperationResult operationResult = new();
            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }
    }
}
