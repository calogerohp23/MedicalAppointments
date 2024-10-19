using MedicalAppointments.Domain.Result;

namespace MedicalAppointments.Persistance.Base
{
    public abstract class ValidatorBase<TEntity> where TEntity: class
    {
        public virtual OperationResult Validate(TEntity entity) 
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null) 
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
                return operationResult;
            }
            if (entity == 0)
            {

            }
        }
    }
}
