using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointments.Domain.Entities.Medical
{
    public sealed class MedicalRecords
    {
        public int RecordId {  get; set; }
        public int PatientID {  get; set; }
        public int DoctorID {  get; set; }
        public string Diagnosis {  get; set; }
        public string Treatment {  get; set; }

    }
}
