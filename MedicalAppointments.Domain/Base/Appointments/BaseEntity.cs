using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointments.Domain.Base.Appointments
{
    public abstract class BaseEntity
    {
        public int DoctorId {  get; set; }
    }
}
