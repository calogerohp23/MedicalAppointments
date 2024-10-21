using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Medical;
using MedicalAppointments.Persistance.Models.Medical;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class MedicalRecordsRepository : BaseRepository<MedicalRecords>, IMedicalRecordsRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<MedicalRecordsRepository> logger;

        public MedicalRecordsRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<MedicalRecordsRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(MedicalRecords entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = true;
                operationResult.Message = "There was an error saving the Medical Record";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        
        public async override Task<OperationResult> Update(MedicalRecords entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                await base.Update(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = true;
                operationResult.Message = "There was an error updating the Medical Record";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();
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
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new OperationResult();
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
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error obtaing the medical records";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}