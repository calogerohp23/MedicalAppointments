namespace MedicalAppointments.Domain.Entities
{
    public sealed class Doctor : User
    {
        private readonly List<AvailabilitySlot> _availability = [];
        public IReadOnlyCollection<AvailabilitySlot> Availability => _availability;
        public Doctor(string fullname, string email, string passwordHash)
            : base(fullname, email, passwordHash)
        {
            Role = UserRole.Doctor;
        }
        public void AddAvailabilitySlot(DateTime from, DateTime to)
        {
            _availability.Add(new AvailabilitySlot(from, to));
        }
        public void RemoveAvailabilitySlot(DateTime from, DateTime to)
        {
            var slot = _availability.FirstOrDefault(s => s.From == from && s.To == to);
            if (slot != null)
            {
                _availability.Remove(slot);
            }
        }
    }
}