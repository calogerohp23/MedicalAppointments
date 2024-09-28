
using MedicalAppointments.Domain.Base.Appointments;

namespace MedicalAppointments.Domain.Entities.Medical
{
    public sealed class AvailabilityModes: BaseEntity
    {
        public int SAvailabilityModeId {  get; set; }
        public string AvailabilityMode {  get; set; }

    }
}
