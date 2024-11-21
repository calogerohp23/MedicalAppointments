using MedicalAppointment.Application.Base;
using MedicalAppointment.Application.Dtos.Appointments.Appointment;
using MedicalAppointment.Application.Response.Appointments.Appointment;

namespace MedicalAppointment.Application.Contracts.Appointment
{
    public interface IAppointmentService : IBaseService<AppointmentResponse, AppointmentSaveDto, AppointmentUpdateDto>
    {
    }
}
