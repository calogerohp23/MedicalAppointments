﻿using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Users;
using MedicalAppointments.Persistance.Models.Users;
using MedicalAppointments.Persistance.Validators.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace MedicalAppointments.Persistance.Repositories.Users
{
    public class PatientsRepository : BaseRepository<Patients>, IPatientsRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<PatientsRepository> logger;
        private readonly PatientsValidator _validator;

        public PatientsRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<PatientsRepository> logger, PatientsValidator validator) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _validator = validator;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Patients entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateSave(entity);
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the patient";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id, Patients entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateUpdate(id, entity);
            try
            {
                Patients? patientsToUpdate = await _medicalAppointmentContext.Patients.FindAsync(id);
                patientsToUpdate.DateOfBirth = entity.DateOfBirth;
                patientsToUpdate.Gender = entity.Gender;
                patientsToUpdate.PhoneNumber = entity.PhoneNumber;
                patientsToUpdate.Address = entity.Address;
                patientsToUpdate.EmergencyContactName = entity.EmergencyContactName;
                patientsToUpdate.EmergencyContactPhone = entity.EmergencyContactPhone;
                patientsToUpdate.BloodType = entity.BloodType;
                patientsToUpdate.Allergies = entity.Allergies;
                patientsToUpdate.InsuranceProviderID = entity.InsuranceProviderID;
                patientsToUpdate.CreatedAt = entity.CreatedAt;
                patientsToUpdate.CreatedBy = entity.CreatedBy;
                patientsToUpdate.IsActive = entity.IsActive;
                patientsToUpdate.UserID = entity.UserID;
                await _medicalAppointmentContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(int id, Patients entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateRemove(id, entity);
            try
            {
                Patients? patientsToRemove = await _medicalAppointmentContext.Patients.FindAsync(id);
                patientsToRemove.IsActive = false;
                patientsToRemove.UpdatedAt = entity.UpdatedAt;
                patientsToRemove.UpdatedBy = entity.UpdatedBy;
                await _medicalAppointmentContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from patients in _medicalAppointmentContext.Patients
                                              join insuranceProvider in _medicalAppointmentContext.InsuranceProviders on patients.InsuranceProviderID equals insuranceProvider.InsuranceProviderID
                                              join users in _medicalAppointmentContext.Users on patients.UserID equals users.UserID
                                              join netowrkType in _medicalAppointmentContext.NetworkType on insuranceProvider.NetworkTypeID equals netowrkType.NetworkTypeID
                                              join createdUser in _medicalAppointmentContext.Users on patients.CreatedBy equals createdUser.UserID
                                              join updatedUser in _medicalAppointmentContext.Users on patients.UpdatedBy equals updatedUser.UserID
                                              where patients.IsActive == true
                                              select new PatientInsuranceUserInsuranceNetworkModel()
                                              {
                                                  PatientID = patients.PatientID,
                                                  FullName = users.FirstName + " " + users.LastName,
                                                  Gender = patients.Gender,
                                                  PhoneNumber = patients.PhoneNumber,
                                                  Address = patients.Address,
                                                  EmergencyContactName = patients.EmergencyContactName,
                                                  EmergencyContactPhone = patients.EmergencyContactPhone,
                                                  BloodType = patients.BloodType,
                                                  Allergies = patients.Allergies,
                                                  InsuranceProviderName = insuranceProvider.Name,
                                                  InsuranceProviderCustomerSupportContact = insuranceProvider.CustomerSupportContact,
                                                  InsuranceProviderAcceptedRegions = insuranceProvider.AcceptedRegions,
                                                  InsuranceProviderMaxCoverage = insuranceProvider.MaxCoverageAmount,
                                                  NetworkTypeName = netowrkType.Name,
                                                  NetworkTypeDescription = netowrkType.Description,
                                                  CreatedAt = patients.CreatedAt,
                                                  UpdatedAt = patients.UpdatedAt,
                                                  CreatedBy = createdUser.FirstName + " " + createdUser.LastName,
                                                  UpdatedBy = updatedUser.FirstName + " " + updatedUser.LastName,
                                              }).AsNoTracking()
                                              .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new();
            _validator.ValidateID(id);
            try
            {
                operationResult.Data = await (from patients in _medicalAppointmentContext.Patients
                                              join insuranceProvider in _medicalAppointmentContext.InsuranceProviders on patients.InsuranceProviderID equals insuranceProvider.InsuranceProviderID
                                              join users in _medicalAppointmentContext.Users on patients.UserID equals users.UserID
                                              join netowrkType in _medicalAppointmentContext.NetworkType on insuranceProvider.NetworkTypeID equals netowrkType.NetworkTypeID
                                              join createdUser in _medicalAppointmentContext.Users on patients.CreatedBy equals createdUser.UserID
                                              join updatedUser in _medicalAppointmentContext.Users on patients.UpdatedBy equals updatedUser.UserID
                                              where patients.IsActive == true && patients.PatientID == id
                                              select new PatientInsuranceUserInsuranceNetworkModel()
                                              {
                                                  PatientID = patients.PatientID,
                                                  FullName = users.FirstName + " " + users.LastName,
                                                  Gender = patients.Gender,
                                                  PhoneNumber = patients.PhoneNumber,
                                                  Address = patients.Address,
                                                  EmergencyContactName = patients.EmergencyContactName,
                                                  EmergencyContactPhone = patients.EmergencyContactPhone,
                                                  BloodType = patients.BloodType,
                                                  Allergies = patients.Allergies,
                                                  InsuranceProviderName = insuranceProvider.Name,
                                                  InsuranceProviderCustomerSupportContact = insuranceProvider.CustomerSupportContact,
                                                  InsuranceProviderAcceptedRegions = insuranceProvider.AcceptedRegions,
                                                  InsuranceProviderMaxCoverage = insuranceProvider.MaxCoverageAmount,
                                                  NetworkTypeName = netowrkType.Name,
                                                  NetworkTypeDescription = netowrkType.Description,
                                                  CreatedAt = patients.CreatedAt,
                                                  UpdatedAt = patients.UpdatedAt,
                                                  CreatedBy = createdUser.FirstName + " " + createdUser.LastName,
                                                  UpdatedBy = updatedUser.FirstName + " " + updatedUser.LastName,
                                              }).AsNoTracking()
                                .ToListAsync();
                operationResult.Data = _validator.ValidateNullData(operationResult.Data);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}