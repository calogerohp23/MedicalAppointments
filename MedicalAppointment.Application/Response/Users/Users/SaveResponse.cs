using MedicalAppointment.Application.Core;

namespace MedicalAppointment.Application.Response.Users.Users
{
    public sealed class SaveResponse : BaseResponse
    {
        public int UserId { get; set; }
    }
}
