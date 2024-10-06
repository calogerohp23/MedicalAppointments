namespace MedicalAppointments.Domain.Base.Users
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber {  get; set; }
    }
}
