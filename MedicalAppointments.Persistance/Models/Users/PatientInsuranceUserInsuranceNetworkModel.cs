namespace MedicalAppointments.Persistance.Models.Users
{
    public sealed class PatientInsuranceUserInsuranceNetworkModel
    {
        public int PatientID { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
        public string? InsuranceProviderName { get; set; }
        public string? InsuranceProviderCustomerSupportContact { get; set; }
        public string? InsuranceProviderAcceptedRegions { get; set; }
        public decimal InsuranceProviderMaxCoverage { get; set; }
        public string? NetworkTypeName { get; set; }
        public string? NetworkTypeDescription { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
