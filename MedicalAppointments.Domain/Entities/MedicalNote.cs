namespace MedicalAppointments.Domain.Entities
{
    public class MedicalNote(string content)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Content { get; private set; } = content;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    }
}
