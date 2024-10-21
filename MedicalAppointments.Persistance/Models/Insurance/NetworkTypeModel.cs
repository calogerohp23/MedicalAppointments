namespace MedicalAppointments.Persistance.Models.Insurance
{
    public sealed class NetworkTypeModel
    {
        public int NetworkTypeID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
