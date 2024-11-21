using MedicalAppointment.Application.Contracts.Appointment;
using MedicalAppointment.Application.Dtos.Appointments.Appointment;
using MedicalAppointment.Application.Response.Appointments.Appointment;
using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Persistance.Interfaces.Appointment;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Application.Services.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        public readonly IAppointmentRepository _appointmentRepository;
        public readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IAppointmentRepository appointmentRepository, ILogger<AppointmentService> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
            ArgumentNullException.ThrowIfNull(appointmentRepository);
        }

        public async Task<AppointmentResponse> GetAll()
        {
            AppointmentResponse response = new();

            try
            {
                var result = await _appointmentRepository.GetAll();

                List<AppointmentGetAllDto> appointment = ((List<Appointment>)result.Data)
                                                        .Select(appointment => new AppointmentGetAllDto()
                                                        {
                                                            AppointmentID = appointment.AppointmentID,
                                                            PatientID = appointment.PatientID,
                                                            DoctorID = appointment.DoctorID,
                                                            AppointmentDate = appointment.AppointmentDate,
                                                        }).OrderByDescending(appointment=> appointment.AppointmentDate).
                                                        ToList();
                response.IsSuccess = result.Success;
                response.Model = appointment;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "The appointment registry couldn't be obtained.";
                _logger.LogError(response.Message, ex.ToString());
            }

            return response;

        }

        public async Task<AppointmentResponse> GetById(int id)
        {
            AppointmentResponse response = new();
            try
            {
                var result = await _appointmentRepository.GetEntityBy(id);
                
                List<AppointmentGetAllDto> appointment = ((List<Appointment>)result.Data)
                                        .Select(appointment => new AppointmentGetAllDto()
                                        {
                                            AppointmentID = appointment.AppointmentID,
                                            PatientID = appointment.PatientID,
                                            DoctorID = appointment.DoctorID,
                                            AppointmentDate = appointment.AppointmentDate,
                                        }).OrderByDescending(appointment => appointment.AppointmentDate).
                                        ToList();
                response.IsSuccess = result.Success;
                response.Model = appointment;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "The specified appointment couldn't be obtained.";
                _logger.LogError(response.Message, ex.ToString());
            }

            return response;
        }

        public async Task<AppointmentResponse> SaveAsync(AppointmentSaveDto dto)
        {
            AppointmentResponse response = new();

            try
            {
                Appointment appointment = new()
                {
                    AppointmentID = dto.AppointmentID,
                    PatientID = dto.PatientID,
                    DoctorID = dto.DoctorID,
                    AppointmentDate = dto.AppointmentDate
                };

                var result = await _appointmentRepository.Save(appointment);
                response.IsSuccess = result.Success;
                response.Model = appointment;
            }
            catch (Exception ex) 
            {
                response.IsSuccess = false;
                response.Message = "The appointment couldn't be saved.";
                _logger.LogError(response.Message, ex.ToString());
            }

            return response;
        }

        public async Task<AppointmentResponse> UpdateAsync(int id, AppointmentUpdateDto dto)
        {
            AppointmentResponse response = new();
            try
            {
                Appointment appointment = new()
                {
                    AppointmentID = dto.AppointmentID,
                    PatientID = dto.PatientID,
                    DoctorID = dto.DoctorID,
                    AppointmentDate = dto.AppointmentDate
                };

                var result = await _appointmentRepository.Update(id, appointment);
                response.IsSuccess = result.Success;
                response.Model = appointment;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "The appointment couldn't be updated.";
                _logger.LogError(response.Message, ex.ToString());
            }

            return response;
        }
    }
}
