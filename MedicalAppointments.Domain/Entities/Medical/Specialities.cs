using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointments.Domain.Entities.Medical
{
    public sealed class Specialities
    {
        public int SpecialtyID {  get; set; }
        public string SpecialtyName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive {  get; set; }
    }
}
