using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.Users
{
    public class DoctorsValidator : BaseValidator<Doctors>
    {
        public override OperationResult ValidateRemove(int id, Doctors entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }

        public override OperationResult ValidateSave(Doctors entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);
            if (entity.LicenseExpirationDate < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                operationResult.Success = false;
                operationResult.Message = "The Doctor's licence is already expired";
                return operationResult;
            }
            return operationResult;
        }

        public override OperationResult ValidateUpdate(int id, Doctors entity)
        {
            OperationResult operationResult = new();
            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }
    }
}
