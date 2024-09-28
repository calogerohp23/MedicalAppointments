
namespace MedicalAppointments.Domain.Entities.Users
{
    public sealed class Doctors
    {
        public int DoctorId {  get; set; }
        public int SpecialtyId {  get; set; }
        public string LicenseNumber {  get; set; }
        public string PhoneNumber {  get; set; }
        public int YearsOfExperieence {  get; set; }
        public string Education {  get; set; }
        public string? Bio {  get; set; }
        public decimal? ConsultationFee { get; set; }
        public string ClinicAddress {  get; set; }
        public int AvailabilityModeId {  get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
