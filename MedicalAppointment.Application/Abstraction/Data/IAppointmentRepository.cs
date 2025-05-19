using MedicalAppointments.Domain.Entities;

namespace MedicalAppointments.Application.Abstraction.Data
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
        Task<Appointment>GetByIdAsync(Guid id);
        Task<IEnumerable<Appointment>> GetPatientIdAsync(Guid patientId);
        Task<IEnumerable<Appointment>> GetDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<Appointment>> GetByDateRangeAsyn(DateTime startDate,DateTime endDate);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Guid id);
        Task<bool> ExistAsync(Guid doctorId, DateTime date);

    }
}
