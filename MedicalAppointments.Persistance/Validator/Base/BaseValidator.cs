using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MedicalAppointments.Persistance.Validator.Base
{
    public abstract class BaseValidator<TEntity> where TEntity : class
    {
        public abstract Task ValidateAsync(TEntity entity);

        protected void ValidateRequiredField(string fieldValue, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldValue))
            {
                throw new ValidationException($"{fieldName} es obligatorio.");
            }
        }

        protected void ValidateInRange(decimal value, decimal min, decimal max, string fieldName)
        {
            if (value < min || value > max)
            {
                throw new ValidationException($"{fieldName} debe estar entre {min} y {max}.");
            }
        }

        protected void ValidateEmailFormat(string email, string fieldName)
        {
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ValidationException($"{fieldName} no tiene un formato válido.");
            }
        }

        protected void ValidatePositiveNumber(decimal number, string fieldName)
        {
            if (number <= 0)
            {
                throw new ValidationException($"{fieldName} debe ser un número positivo.");
            }
        }

        protected void ValidateStringLength(string value, int minLength, int maxLength, string fieldName)
        {
            if (value.Length < minLength || value.Length > maxLength)
            {
                throw new ValidationException($"{fieldName} debe tener entre {minLength} y {maxLength} caracteres.");
            }
        }

        protected void ValidateDateInFuture(DateTime date, string fieldName)
        {
            if (date < DateTime.Now)
            {
                throw new ValidationException($"{fieldName} debe ser una fecha futura.");
            }
        }

        protected async Task ValidateUniqueFieldAsync(string fieldValue, Func<string, Task<bool>> checkExistence, string fieldName)
        {
            if (await checkExistence(fieldValue))
            {
                throw new ValidationException($"{fieldName} ya existe.");
            }
        }

        protected async Task ValidateExistingReferenceAsync(Guid referenceId, Func<Guid, Task<bool>> checkExistence, string fieldName)
        {
            if (!await checkExistence(referenceId))
            {
                throw new ValidationException($"{fieldName} no existe.");
            }
        }
    }

}

