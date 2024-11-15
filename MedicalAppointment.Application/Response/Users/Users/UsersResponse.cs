
using MedicalAppointment.Application.Core;

namespace MedicalAppointment.Application.Response.Users.Users
{
    public class UsersResponse : BaseResponse
    {
        public dynamic? Model { get; set; }
        public int UserId { get; set; }
    }
}
