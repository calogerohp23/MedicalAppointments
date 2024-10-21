﻿namespace MedicalAppointments.Persistance.Models.Medical
{
    public sealed class MedicalRecordsPatientsDoctorsModel
    {
        public int RecordID { get; set; }
        public int PatientID { get; set; }
        public string? PatientName { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public int DoctorID { get; set; }
        public string? DoctorName { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
