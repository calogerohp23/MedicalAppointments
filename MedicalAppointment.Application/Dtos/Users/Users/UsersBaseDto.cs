namespace MedicalAppointment.Application.Dtos.Users.Users
{
    public abstract class UsersBaseDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int UserChange { get; set; }
        public int CreatedUser { get; set; }
    
    }
}
