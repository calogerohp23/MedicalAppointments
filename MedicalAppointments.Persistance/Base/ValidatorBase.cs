using MedicalAppointments.Domain.Result;

namespace MedicalAppointments.Persistance.Base
{
    public abstract class ValidatorBase
    {
        public virtual OperationResult EntitityNull(object entity)
        {
            OperationResult operationResult = new OperationResult();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null.";
            }
            return operationResult;

        }
        public virtual OperationResult EqualOrLessThanZero(int property, string propertyName)
        {
            OperationResult operationResult = new OperationResult();
            if (property <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = $"The {propertyName} is equal or less than zero.";
            }
            return operationResult;

        }
        public virtual OperationResult EqualOrLess(int property, int comparisonAmount, string propertyName)
        {
            OperationResult operationResult = new OperationResult();
            if (property <= comparisonAmount)
            {
                operationResult.Success = false;
                operationResult.Message = $"The {propertyName} is less than {comparisonAmount}.";
            }
            return operationResult;

        }

        public virtual OperationResult LessThan(int property, int comparisonAmount, string propertyName)
        {
            OperationResult operationResult = new OperationResult();
            if (property < comparisonAmount)
            {
                operationResult.Success = false;
                operationResult.Message = $"The {propertyName} is less than {comparisonAmount}.";
            }
            return operationResult;
        }

        public virtual OperationResult GreaterOrEqual(int property, int comparisonAmount, string propertyName)
        {
            OperationResult operationResult = new OperationResult();
            if (property >= comparisonAmount)
            {
                operationResult.Success = false;
                operationResult.Message = $"The {propertyName} is greater than {comparisonAmount}.";
            }
            return operationResult;

        }

        public virtual OperationResult GreaterThan(int property, int comparisonAmount, string propertyName)
        {
            OperationResult operationResult = new OperationResult();
            if (property > comparisonAmount)
            {
                operationResult.Success = false;
                operationResult.Message = $"The {propertyName} is greater than {comparisonAmount}.";
            }
            return operationResult;

        }
        public virtual OperationResult Inequality(int property, int comparisonAmount, string message)
        {
            OperationResult operationResult = new OperationResult();
            if (property != comparisonAmount)
            {
                operationResult.Success = false;
                operationResult.Message = message;
            }
            return operationResult;

        }
        public virtual OperationResult Equal(int property, int comparisonAmount, string message)
        {
            OperationResult operationResult = new OperationResult();
            if (property == comparisonAmount)
            {
                operationResult.Success = false;
                operationResult.Message = message;
            }
            return operationResult;

        }
        public virtual OperationResult StringNull(string property, string propertyName)
        {
            OperationResult operationResult = new OperationResult();
            if (string.IsNullOrEmpty(property))
            {
                operationResult.Success = false;
                operationResult.Message = $"The {propertyName} is required.";
            }
            return operationResult;

        }
        public virtual OperationResult PastDate(DateTime property, DateTime comparisonDate, string propertyName)
        {
            OperationResult operationResult = new OperationResult();
            if (property <= comparisonDate)
            {
                operationResult.Success = false;
                operationResult.Message = $"The {propertyName} is a past date.";
            }
            return operationResult;
        }
        public virtual OperationResult FutureDate(DateTime property, DateTime comparisonDate, string propertyName)
        {
            OperationResult operationResult = new OperationResult();
            if (property >= comparisonDate)
            {
                operationResult.Success = false;
                operationResult.Message = $"The {propertyName} is a future date.";
            }
            return operationResult;
        }

        public virtual OperationResult TimeLessOrEqualThan(TimeOnly property, TimeOnly comparisonDate, string message)
        {
            OperationResult operationResult = new OperationResult();
            if (property <= comparisonDate)
            {
                operationResult.Success = false;
                operationResult.Message = message;
            }
            return operationResult;
        }
        public virtual OperationResult True(bool property, string message)
        {
            OperationResult operationResult = new OperationResult();
            if (property == true)
            {
                operationResult.Success = false;
                operationResult.Message = message;
            }
            return operationResult;
        }
        public virtual OperationResult False(bool property, string message)
        {
            OperationResult operationResult = new OperationResult();
            if (property == false)
            {
                operationResult.Success = false;
                operationResult.Message = message;
            }
            return operationResult;
        }

    }
}
