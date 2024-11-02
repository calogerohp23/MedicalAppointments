using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Users
{
    [Table("Doctors", Schema = "users")]
    public sealed class Doctors : Base.BaseEntity
    {
        [Key]
        public int DoctorID { get; private set; }
        public int UserID { get; set; }
        public int SpecialtyId { get; set; }
        public string? LicenseNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Education { get; set; }
        public string? Bio { get; set; }
        public decimal ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public int AvailabilityModeId { get; set; }
        public DateOnly? LicenseExpirationDate { get; set; }
    }
}
