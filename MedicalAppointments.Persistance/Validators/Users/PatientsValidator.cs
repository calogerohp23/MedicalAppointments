using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.Users
{
    public class PatientsValidator :BaseValidator<Patients>
    {
        public override OperationResult ValidateRemove(int id, Patients entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }

        public override OperationResult ValidateSave(Patients entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);

            return operationResult;
        }

        public override OperationResult ValidateUpdate(int id, Patients entity)
        {
            OperationResult operationResult = new();
            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }
    }
}
