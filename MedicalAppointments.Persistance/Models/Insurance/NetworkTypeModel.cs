namespace MedicalAppointments.Persistance.Models.Insurance
{
    public sealed class NetworkTypeModel
    {
        public int NetworkTypeID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
