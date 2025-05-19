namespace MedicalAppointments.Domain.Entities
{
    public class Patient : User
    {
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }
        public List<MedicalNote> Notes { get; private set; } = [];
        public Patient(string fullname, string email, string passwordHash, string phoneNumber, string address)
            : base(fullname, email, passwordHash)
        {
            Role = UserRole.Patient;
            PhoneNumber = phoneNumber;
            Address = address;
        }
        public void AddNote(string content)
        {
            Notes.Add(new MedicalNote(content));
        }
    }
}
