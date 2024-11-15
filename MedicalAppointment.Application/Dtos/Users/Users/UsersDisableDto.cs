namespace MedicalAppointment.Application.Dtos.Users.Users
{
    public class UsersDisableDto : UsersBaseDto
    {
        public int UserID { get; private set; }
        public bool? IsActive { get; set; }
    }
}
