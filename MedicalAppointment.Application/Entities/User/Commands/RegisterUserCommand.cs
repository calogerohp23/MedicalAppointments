using MediatR;

namespace MedicalAppointments.Application.Entities.User.Commands
{
    public class RegisterUserCommand: IRequest<Guid>
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }
}
