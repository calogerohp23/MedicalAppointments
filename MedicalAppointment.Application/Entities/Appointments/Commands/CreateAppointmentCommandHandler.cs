using MediatR;
using MedicalAppointments.Domain.Entities;
using MedicalAppointments.Application.Abstraction.Data;


namespace MedicalAppointments.Application.Entities.Appointments.Commands
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
    {
        private readonly IAppointmentRepository _repository;
        public CreateAppointmentCommandHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = new Appointment(request.PatientId, request.DoctorId, request.ScheduledDate);

            await _repository.AddAsync(appointment);
            return appointment.Id;
        }
    }
}
