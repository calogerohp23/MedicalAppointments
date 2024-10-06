using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Users
{
    [Table("Patients", Schema = "users")]
    public sealed class Patients : Base.Users.BaseEntity
    {
        [Key]
        public int PatientId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address {  get; set; }
        public string EmergencyContactName {  get; set; }
        public string EmergencyContactPhone {  get; set; }
        public string BloodType {  get; set; }
        public string Allergies {  get; set; }
        public int InsuranceProviderID {  get; set; }
    }
}
