namespace MedicalAppointment.Application.Dtos.Users.Users
{
    public class UsersSaveDto : UsersBaseDto
    {
        public int UserID { get; private set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
