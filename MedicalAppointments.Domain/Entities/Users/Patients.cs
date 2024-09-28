

namespace MedicalAppointments.Domain.Entities.Users
{
    public sealed class Patients
    {
        public int PatientId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber {  get; set; }
        public string Address {  get; set; }
        public string EmergencyContactName {  get; set; }
        public string EmergencyContactPhone {  get; set; }
        public string BloodType {  get; set; }
        public string Allergies {  get; set; }
        public int InsuranceProviderID {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
