using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;

namespace MedicalAppointments.Persistance.Validators.Insurance
{
    public class InsuranceProvidersValidator : BaseValidator<InsuranceProviders>
    {
        public override OperationResult ValidateRemove(int id, InsuranceProviders entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }

        public override OperationResult ValidateSave(InsuranceProviders entity)
        {
            OperationResult operationResult = new();

            base.ValidateNull(entity);

            return operationResult;
        }

        public override OperationResult ValidateUpdate(int id, InsuranceProviders entity)
        {
            OperationResult operationResult = new();
            base.ValidateNull(entity);
            base.ValidateID(id);

            return operationResult;
        }
    }
}
