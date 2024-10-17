using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Insurance;
using MedicalAppointments.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class InsuranceProvidersRepository : BaseRepository<InsuranceProviders>, IInsuranceProvidersRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<InsuranceProvidersRepository> logger;

        public InsuranceProvidersRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<InsuranceProvidersRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async Task<OperationResult> Save(InsuranceProviders entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es nula";
                return operationResult;
            }
            if (await base.Exists(insurance => insurance.Email == entity.Email))
            {
                operationResult.Success = false;
                operationResult.Message = "El email se encuentra registrado";
                return operationResult;
            }

            try
            {
                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Hubo un error guardando la entidad";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        public async override Task<OperationResult> Update(InsuranceProviders entity)
        {
            OperationResult operationResult = new OperationResult();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es nula";
                return operationResult;
            }
            try
            {
                InsuranceProviders? insuranceProvidersToUpdate = await _medicalAppointmentContext.InsuranceProviders.FindAsync(entity.InsuranceProviderID);
                insuranceProvidersToUpdate.Name = entity.Name;
                insuranceProvidersToUpdate.ContactNumber = entity.ContactNumber;
                insuranceProvidersToUpdate.Email = entity.Email;
                insuranceProvidersToUpdate.Website = entity.Website;
                insuranceProvidersToUpdate.Address = entity.Address;
                insuranceProvidersToUpdate.City = entity.City;
                insuranceProvidersToUpdate.State = entity.State;
                insuranceProvidersToUpdate.Country = entity.Country;
                insuranceProvidersToUpdate.ZipCode = entity.ZipCode;
                insuranceProvidersToUpdate.CoverageDetails = entity.CoverageDetails;
                insuranceProvidersToUpdate.LogoUrl = entity.LogoUrl;
                insuranceProvidersToUpdate.IsPreferred = entity.IsPreferred;
                insuranceProvidersToUpdate.NetworkTypeID = entity.NetworkTypeID;
                insuranceProvidersToUpdate.CustomerSupportContact = entity.CustomerSupportContact;
                insuranceProvidersToUpdate.AcceptedRegions = entity.AcceptedRegions;
                insuranceProvidersToUpdate.MaxCoverageAmount = entity.MaxCoverageAmount;
                insuranceProvidersToUpdate.CreatedAt = entity.CreatedAt;
                insuranceProvidersToUpdate.UpdatedAt = entity.UpdatedAt;
                insuranceProvidersToUpdate.IsActive = entity.IsActive;
                insuranceProvidersToUpdate.UpdatedBy = entity.UpdatedBy;
                insuranceProvidersToUpdate.CreatedBy = entity.CreatedBy;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Hubo un error actualizando el proveedor";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(InsuranceProviders entity)
        {
            OperationResult operationResult = new OperationResult();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es nula";
                return operationResult;
            }
            try
            {
                InsuranceProviders? insuranceProvidersToRemove = await _medicalAppointmentContext.InsuranceProviders.FindAsync(entity.InsuranceProviderID);
                insuranceProvidersToRemove.IsActive = false;
                insuranceProvidersToRemove.UpdatedAt = entity.UpdatedAt;
                insuranceProvidersToRemove.UpdatedBy = entity.UpdatedBy;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Hubo un error removiendo el proveedor";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                operationResult.Data = await (from insuranceProviders in _medicalAppointmentContext.InsuranceProviders
                                              join networkType in _medicalAppointmentContext.NetworkType on insuranceProviders.NetworkTypeID equals networkType.NetworkTypeID
                                              where insuranceProviders.IsActive == true
                                              orderby insuranceProviders.CreatedAt descending
                                              select new InsuranceProvidersNetworkTypeModel()
                                              {

                                              }).ToListAsync();
            }
            catch (Exception ex)
            {

            }

            return operationResult;

        }
    }
}
