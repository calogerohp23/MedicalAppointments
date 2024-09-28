
namespace MedicalAppointments.Domain.Entities.Insurance
{
    public sealed class NetworkType: Base.Insurance.BaseEntity
    {
        public int NetworkTypeId {  get; set; }
        public string? Description {  get; set; }
    }
}
