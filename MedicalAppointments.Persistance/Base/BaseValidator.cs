using MedicalAppointments.Domain.Result;

namespace MedicalAppointments.Persistance.Base
{
    public abstract class BaseValidator<TEntity>()
    {

        public virtual OperationResult ValidateNull(TEntity entity)
        {
            OperationResult operationResult = new OperationResult();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
                return operationResult;
            }
            return operationResult;
        }

        public virtual OperationResult ValidateNullData(dynamic data)
        {
            OperationResult operationResult = new OperationResult();
            if (data == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The ID does not exist in the database";
                return operationResult;
            }
            return data;
        }
        public virtual OperationResult ValidateID(int id)
        {
            OperationResult operationResult = new OperationResult();
            if (id <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "The ID must be positive or an integer";
                return operationResult;
            }
            return operationResult;
        }
        public abstract OperationResult ValidateSave(TEntity entity);
        public abstract OperationResult ValidateUpdate(int id, TEntity entity);
        public abstract OperationResult ValidateRemove(int id, TEntity entity);

    }
}
