﻿using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Insurance;
using MedicalAppointments.Persistance.Models;
using MedicalAppointments.Persistance.Models.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class InsuranceProvidersRepository : BaseRepository<InsuranceProviders>, IInsuranceProvidersRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<InsuranceProvidersRepository> logger;
        private readonly ValidatorBase _validator;

        public InsuranceProvidersRepository(MedicalAppointmentContext medicalAppointmentContext, ValidatorBase validator, ILogger<InsuranceProvidersRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _validator = validator;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(InsuranceProviders entity)
        {
<<<<<<< Updated upstream
            OperationResult operationResult = new();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
                return operationResult;
            }
=======
            OperationResult operationResult = new OperationResult();
            _validator.EntitityNull(entity);
            _validator.EqualOrLessThanZero(entity.NetworkTypeID,"NetworkTypeID");

>>>>>>> Stashed changes
            try
            {
                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the Insurance Provider.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        public async override Task<OperationResult> Update(InsuranceProviders entity)
        {
            OperationResult operationResult = new();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
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
                insuranceProvidersToUpdate.UpdatedAt = entity.UpdatedAt;
                insuranceProvidersToUpdate.IsActive = entity.IsActive;
                insuranceProvidersToUpdate.UpdatedBy = entity.UpdatedBy;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error updating the Insurance provider.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(InsuranceProviders entity)
        {
            OperationResult operationResult = new();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
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
                operationResult.Message = "There was an error removing the Insurance provider.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from insuranceProviders in _medicalAppointmentContext.InsuranceProviders
                                              join networkType in _medicalAppointmentContext.NetworkType on insuranceProviders.NetworkTypeID equals networkType.NetworkTypeID
                                              where insuranceProviders.IsActive == true
                                              orderby insuranceProviders.CreatedAt descending
                                              select new InsuranceProvidersNetworkTypeModel()
                                              {
                                                  Name = insuranceProviders.Name,
                                                  ContactNumber = insuranceProviders.ContactNumber,
                                                  Email = insuranceProviders.Email,
                                                  Website = insuranceProviders.Website,
                                                  Address = insuranceProviders.Address,
                                                  City = insuranceProviders.City,
                                                  State = insuranceProviders.State,
                                                  Country = insuranceProviders.Country,
                                                  ZipCode = insuranceProviders.ZipCode,
                                                  CoverageDetails = insuranceProviders.CoverageDetails,
                                                  LogoUrl = insuranceProviders.LogoUrl,
                                                  IsPreferred = insuranceProviders.IsPreferred,
                                                  NetworkTypeID = networkType.NetworkTypeID,
                                                  NetworkDescription = networkType.Description,
                                                  CustomerSupportContact = insuranceProviders.CustomerSupportContact,
                                                  AcceptedRegion = insuranceProviders.AcceptedRegions,
                                                  MaxCoverageAmount = insuranceProviders.MaxCoverageAmount,
                                                  CreatdAt = insuranceProviders.CreatedAt,
                                                  UpdatedAt = insuranceProviders.UpdatedAt,
                                                  CreatedBy = insuranceProviders.CreatedBy,
                                                  UpdatedBy = insuranceProviders.UpdatedBy,

                                              }).AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaining the Insurance Providers.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new();
            if(id == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "The insurance provider does not exist";
                return operationResult;
            }
            try
            {
                operationResult.Data = await (from insuranceProviders in _medicalAppointmentContext.InsuranceProviders
                                              join networkType in _medicalAppointmentContext.NetworkType on insuranceProviders.NetworkTypeID equals networkType.NetworkTypeID
                                              where insuranceProviders.IsActive == true && insuranceProviders.InsuranceProviderID == id
                                              orderby insuranceProviders.CreatedAt descending
                                              select new InsuranceProvidersNetworkTypeModel()
                                              {
                                                  Name = insuranceProviders.Name,
                                                  ContactNumber = insuranceProviders.ContactNumber,
                                                  Email = insuranceProviders.Email,
                                                  Website = insuranceProviders.Website,
                                                  Address = insuranceProviders.Address,
                                                  City = insuranceProviders.City,
                                                  State = insuranceProviders.State,
                                                  Country = insuranceProviders.Country,
                                                  ZipCode = insuranceProviders.ZipCode,
                                                  CoverageDetails = insuranceProviders.CoverageDetails,
                                                  LogoUrl = insuranceProviders.LogoUrl,
                                                  IsPreferred = insuranceProviders.IsPreferred,
                                                  NetworkTypeID = networkType.NetworkTypeID,
                                                  NetworkDescription = networkType.Description,
                                                  CustomerSupportContact = insuranceProviders.CustomerSupportContact,
                                                  AcceptedRegion = insuranceProviders.AcceptedRegions,
                                                  MaxCoverageAmount = insuranceProviders.MaxCoverageAmount,
                                                  CreatdAt = insuranceProviders.CreatedAt,
                                                  UpdatedAt = insuranceProviders.UpdatedAt,
                                                  CreatedBy = insuranceProviders.CreatedBy,
                                                  UpdatedBy = insuranceProviders.UpdatedBy,

                                              }).AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaining the specific Insurance Providers.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;


        }
    }
}
