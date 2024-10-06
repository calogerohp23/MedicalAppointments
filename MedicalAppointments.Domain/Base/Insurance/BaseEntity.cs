namespace MedicalAppointments.Domain.Base.Insurance;

public abstract class BaseEntity
{
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public int NetworkTypeID { get; set; }
}

