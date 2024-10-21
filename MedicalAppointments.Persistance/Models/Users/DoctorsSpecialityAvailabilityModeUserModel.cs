namespace MedicalAppointments.Persistance.Models.Users
{
    public sealed class DoctorsSpecialityAvailabilityModeUserModel
    {
        public int DoctorID { get; set; }
        public string? FullName { get; set; }
        public int SpecialtyName { get; set; }
        public string? LicenceNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Education { get; set; }
        public string? Bio { get; set; }
        public decimal ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public string? AvailabilityMode { get; set; }
        public DateOnly? LicenceExpirationDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
