using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointments.Domain.Entities.System
{
    public sealed class Status
    {
        public int StatusId {  get; set; }
        public string StatusName { get; set; }
    }
}
