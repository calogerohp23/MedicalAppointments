using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.System
{
    [Table("Status", Schema = "system")]
    public sealed class Status
    {
        [Key]
        public int StatusId {  get; private set; }
        public string? StatusName { get; set; }
    }
}
