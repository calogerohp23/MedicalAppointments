using MedicalAppointment.Application.Base;
using MedicalAppointment.Application.Dtos.Appointments.DoctorAvailability;
using MedicalAppointment.Application.Response.Appointments.DoctorAvailability;

namespace MedicalAppointment.Application.Contracts.Appointment
{
    public interface IDoctorAvailabilityService : IBaseService<DoctorAvailabilityResponse, DoctorAvailabilitySaveDto,DoctorAvailabilityUpdateDto>
    {
    }
}
