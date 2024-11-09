using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Medical;
using MedicalAppointments.Persistance.Models.Medical;
using MedicalAppointments.Persistance.Validators.Medical;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Medical
{
    public class MedicalRecordsRepository : BaseRepository<MedicalRecords>, IMedicalRecordsRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<MedicalRecordsRepository> logger;
        private readonly MedicalRecordsValidator _validator;

        public MedicalRecordsRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<MedicalRecordsRepository> logger, MedicalRecordsValidator validator) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _validator = validator;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(MedicalRecords entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateSave(entity);
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = true;
                operationResult.Message = "There was an error saving the Medical Record";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id, MedicalRecords entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateUpdate(id, entity);

            try
            {
                MedicalRecords? medicalRecords = await _medicalAppointmentContext.MedicalRecords.FindAsync(id);
                medicalRecords.PatientID = entity.PatientID;
                medicalRecords.DoctorID = entity.DoctorID;
                medicalRecords.Diagnosis = entity.Diagnosis;
                medicalRecords.Treatment = entity.Treatment;
                medicalRecords.DateOfVisit = entity.DateOfVisit;
                medicalRecords.UpdatedAt = entity.UpdatedAt;
                medicalRecords.UpdatedBy = entity.UpdatedBy;
                medicalRecords.IsActive = entity.IsActive;
            }
            catch (Exception ex)
            {
                operationResult.Success = true;
                operationResult.Message = "There was an error updating the Medical Record";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        public async override Task<OperationResult> Remove(int id, MedicalRecords entity)
        {
            OperationResult operationResult = new();
            _validator.ValidateRemove(id, entity);

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
                return operationResult;
            }
            try
            {
                MedicalRecords? medicalRecords = await _medicalAppointmentContext.MedicalRecords.FindAsync(id);
                medicalRecords.UpdatedAt = entity.UpdatedAt;
                medicalRecords.UpdatedBy = entity.UpdatedBy;
                medicalRecords.IsActive = false;

            }
            catch (Exception ex)
            {
                operationResult.Success = true;
                operationResult.Message = "There was an error updating the Medical Record";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from medicalRecords in _medicalAppointmentContext.MedicalRecords
                                              join patients in _medicalAppointmentContext.Patients on medicalRecords.PatientID equals patients.PatientID
                                              join doctors in _medicalAppointmentContext.Doctors on medicalRecords.DoctorID equals doctors.DoctorID
                                              join patientUsers in _medicalAppointmentContext.Users on patients.UserID equals patientUsers.UserID
                                              join doctorUsers in _medicalAppointmentContext.Users on doctors.UserID equals doctorUsers.UserID
                                              orderby medicalRecords.CreatedAt descending
                                              select new MedicalRecordsPatientsDoctorsModel()
                                              {
                                                  RecordID = medicalRecords.RecordId,
                                                  PatientID = patients.PatientID,
                                                  PatientName = patientUsers.FirstName + " " + patientUsers.LastName,
                                                  Diagnosis = medicalRecords.Diagnosis,
                                                  Treatment = medicalRecords.Treatment,
                                                  DateOfVisit = medicalRecords.DateOfVisit,
                                                  DoctorID = doctors.DoctorID,
                                                  DoctorName = doctorUsers.FirstName + " " + doctorUsers.LastName,
                                                  UpdatedAt = medicalRecords.UpdatedAt,
                                                  UpdatedBy = medicalRecords.UpdatedBy,
                                              }).AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaing the medical records";
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
                operationResult.Data = await (from medicalRecords in _medicalAppointmentContext.MedicalRecords
                                              join patients in _medicalAppointmentContext.Patients on medicalRecords.PatientID equals patients.PatientID
                                              join doctors in _medicalAppointmentContext.Doctors on medicalRecords.DoctorID equals doctors.DoctorID
                                              join patientUsers in _medicalAppointmentContext.Users on patients.UserID equals patientUsers.UserID
                                              join doctorUsers in _medicalAppointmentContext.Users on doctors.UserID equals doctorUsers.UserID
                                              orderby medicalRecords.CreatedAt descending
                                              where medicalRecords.RecordId == id
                                              select new MedicalRecordsPatientsDoctorsModel()
                                              {
                                                  RecordID = medicalRecords.RecordId,
                                                  PatientID = patients.PatientID,
                                                  PatientName = patientUsers.FirstName + " " + patientUsers.LastName,
                                                  Diagnosis = medicalRecords.Diagnosis,
                                                  Treatment = medicalRecords.Treatment,
                                                  DateOfVisit = medicalRecords.DateOfVisit,
                                                  DoctorID = doctors.DoctorID,
                                                  DoctorName = doctorUsers.FirstName + " " + doctorUsers.LastName,
                                                  UpdatedAt = medicalRecords.UpdatedAt,
                                                  UpdatedBy = medicalRecords.UpdatedBy,
                                              }).AsNoTracking()
                                            .ToListAsync();
                operationResult.Data = _validator.ValidateNullData(operationResult.Data);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaing the medical records";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}