﻿using MedicalAppointments.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Medical
{
    [Table("MedicalRecords", Schema = "medical")]
    public sealed class MedicalRecords : BaseEntity
    {
        [Key]
        public int RecordId { get; private set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public DateTime? DateOfVisit { get; set; }
    }
}
