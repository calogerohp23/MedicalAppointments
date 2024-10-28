namespace MedicalAppointments.Persistance.Models.Insurance
{
    public class InsuranceProvidersNetworkTypeModel
    {
        public int InsuranceProviderID { get; set; }

        public string? Name { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string? CoverageDetails { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsPreferred { get; set; }
        public int NetworkTypeID { get; set; }
        public string? NetworkDescription { get; set; }
        public string? CustomerSupportContact { get; set; }
        public string? AcceptedRegion { get; set; }
        public decimal? MaxCoverageAmount { get; set; }
        public DateTime? CreatdAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
