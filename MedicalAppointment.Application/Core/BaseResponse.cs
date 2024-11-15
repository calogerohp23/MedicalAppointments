namespace MedicalAppointment.Application.Core
{
    public abstract class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Origin { get; set; }
        public string? Destiny { get; set; }
    }
}
