namespace MedicalAppointments.Domain.Entities
{
    public abstract class User(string fullname, string email, string passwordHash)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FullName { get; private set; } = fullname;
        public string Email { get; private set; } = email;
        public string PasswordHash { get; private set; } = passwordHash;
        public UserRole Role { get; protected set; }

        public void UpdateEmail(string newEmail) => Email = newEmail;
    }
    public enum UserRole
    {
        Patient,
        Doctor,
        Admin
    }
}
