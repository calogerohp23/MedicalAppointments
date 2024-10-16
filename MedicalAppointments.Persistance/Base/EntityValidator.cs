using MedicalAppointments.Domain.Result;

namespace MedicalAppointments.Persistance.Base
{
    public class EntityValidator<TEntity>(OperationResult operationResult) where TEntity : class
    {
        private readonly OperationResult _operationResult = operationResult;

        public OperationResult ValidateNulls(TEntity entity) 
        {
            
            if (entity == null) 
            {
                _operationResult.Success = false;
                _operationResult.Message = $"{entity} is being required";
            }

            return _operationResult;
        }
        public OperationResult ValidateLessEqualZero(TEntity entity, int propiety)
        {

            if (propiety <= 0)
            {
                _operationResult.Success = false;
                _operationResult.Message = $"{entity} is less than zero";
            }

            return _operationResult;

        }

        public OperationResult ValidateEqualZero(TEntity entity, int propiety)
        {

            if (propiety <= 0)
            {
                _operationResult.Success = false;
                _operationResult.Message = $"{entity} equals zero";
            }

            return _operationResult;

        }

    }
}
