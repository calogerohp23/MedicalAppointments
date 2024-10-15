using MedicalAppointments.Domain.Result;

namespace MedicalAppointments.Persistance.Base
{
    public class EntityValidator<TEntity> where TEntity : class
    {
        private readonly OperationResult _operationResult;

        public EntityValidator(OperationResult operationResult) => _operationResult = operationResult;

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

        public async OperationResult ValidateDuplicateAsync(TEntity entity) {
            if (await ) 
            {
            
            }

            return _operationResult;
        }

    }
}
