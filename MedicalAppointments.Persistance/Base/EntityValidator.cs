using MedicalAppointments.Domain.Result;

namespace MedicalAppointments.Persistance.Base
{
    public class EntityValidator<TEntity> where TEntity : class
    {
        public OperationResult ValidateNulls(TEntity entity) 
        {
            OperationResult operationResult = new OperationResult();
            
            if (entity == null) 
            {
                operationResult.Success = false;
                operationResult.Message = $"The entity is being required";
            }

            return operationResult;
        }
        public OperationResult ValidateLessEqualZero(TEntity entity, int propiety)
        {
            OperationResult operationResult = new OperationResult();

            if (propiety <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = $"The entity is being required";
            }

            return operationResult;

        }

        public OperationResult ValidateEqualZero(TEntity entity, int propiety)
        {
            OperationResult operationResult = new OperationResult();

            if (propiety <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = $"The entity equals zero";
            }

            return operationResult;

        }

    }
}
