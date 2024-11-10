using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.Users
{
    public class UsersValidator:BaseValidator<Userss>
    {
        public override OperationResult ValidateRemove(int id, Userss entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }

        public override OperationResult ValidateSave(Userss entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);

            return operationResult;
        }

        public override OperationResult ValidateUpdate(int id, Userss entity)
        {
            OperationResult operationResult = new();
            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }
    }
}
