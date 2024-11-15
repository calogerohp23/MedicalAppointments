namespace MedicalAppointment.Application.Dtos.Users.Users
{
    public class UsersUpdateDto : UsersBaseDto
    {
        public int UserID { get; private set; }
        public bool? IsActive { get; set; }
    }
}
