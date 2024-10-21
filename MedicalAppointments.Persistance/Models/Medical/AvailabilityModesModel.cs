
namespace MedicalAppointments.Persistance.Models.Medical
{
    public sealed class AvailabilityModesModel
    {
        public int SAvailabilityModeID { get; set; }
        public string? AvailabilityMode { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
