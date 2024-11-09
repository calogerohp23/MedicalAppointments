using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.Insurance
{
    public class NetworkTypeValidator : BaseValidator<NetworkType>
    {
        public override OperationResult ValidateRemove(int id, NetworkType entity)
        {
            OperationResult operationResult = new();
            if (!entity.IsActive)
            {
                operationResult.Success = false;
                operationResult.Message = "The network type has already been deleted";
                return operationResult;
            }
            if (entity.NetworkTypeID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "The selected networkt type does not exist";
                return operationResult;
            }
            return operationResult;
        }

        public override OperationResult ValidateSave(NetworkType entity)
        {
            OperationResult operationResult = new();
            if (entity.Description?.Length > 255)
            {
                operationResult.Success = false;
                operationResult.Message = "The description is to long, it can only be top 250 characters.";
                return operationResult;
            }
            return operationResult;
        }

        public override OperationResult ValidateUpdate(int id, NetworkType entity)
        {
            OperationResult operationResult = new();
            if (entity.Description?.Length > 255)
            {
                operationResult.Success = false;
                operationResult.Message = "The description is to long, it can only be top 250 characters.";
                return operationResult;
            }
            if (entity.NetworkTypeID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "The selected networkt type does not exist";
                return operationResult;
            }
            return operationResult;
        }
    }
}
