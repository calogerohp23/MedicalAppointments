namespace MedicalAppointments.Domain.Entities
{
    public class AvailabilitySlot
    {
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

        public AvailabilitySlot(DateTime from, DateTime to)
        {
            if (from >= to)
                throw new ArgumentException("The 'from' date must be earlier than the 'to' date.");

            From = from;
            To = to;
        }
    }
}
