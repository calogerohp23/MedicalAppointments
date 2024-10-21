namespace MedicalAppointments.Persistance.Models.Medical
{
    public sealed class SpecialtiesModel
    {
        public int SpecialtyID { get; set; }
        public string? SpecialtyName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set;}
    }
}
