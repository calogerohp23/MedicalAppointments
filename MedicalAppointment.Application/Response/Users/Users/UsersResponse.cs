
using MedicalAppointment.Application.Core;

namespace MedicalAppointment.Application.Response.Users.Users
{
    public class UsersResponse
    {
        public dynamic? Model { get; set; }
        public int UserId { get; set; }
        public string? Origin { get; set; }
        public string? Destiny { get; set; }
        public bool? IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
