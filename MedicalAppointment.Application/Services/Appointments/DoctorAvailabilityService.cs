using MedicalAppointment.Application.Contracts.Appointment;
using MedicalAppointment.Application.Dtos.Appointments.DoctorAvailability;
using MedicalAppointment.Application.Response.Appointments.DoctorAvailability;
using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Persistance.Interfaces.Appointment;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Application.Services.Appointments
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        public readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        public readonly ILogger<DoctorAvailabilityService> _logger;

        public DoctorAvailabilityService(IDoctorAvailabilityRepository doctorAvailabilityRepository, ILogger<DoctorAvailabilityService> logger)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _logger = logger;
            ArgumentNullException.ThrowIfNull(doctorAvailabilityRepository);
        }

        public async Task<DoctorAvailabilityResponse> GetAll()
        {
            DoctorAvailabilityResponse response = new();
            try
            {
                var result = await _doctorAvailabilityRepository.GetAll();

                List<DoctorAvailabilityGetAllDto> doctorAvailability = ((List<DoctorAvailability>)result.Data)
                                                                        .Select(doctorAvailability => new DoctorAvailabilityGetAllDto()
                                                                        {
                                                                            AvailabilityId = doctorAvailability.AvailabilityId,
                                                                            DoctorId = doctorAvailability.DoctorID,
                                                                            AvailableDate = doctorAvailability.AvailableDate,
                                                                            StarTime = doctorAvailability.StartTime,
                                                                            EndTime = doctorAvailability.EndTime,
                                                                        }).OrderByDescending(doctorAvailability=>doctorAvailability.AvailableDate)
                                                                        .ToList();
                response.IsSuccess = result.Success;
                response.Model = doctorAvailability;
            }
            catch (Exception ex) 
            {
                response.IsSuccess = false;
                response.Message = "The doctor's availability registry couldn't be obtained.";
                _logger.LogError(response.Message, ex.ToString());
            }
            return response;
        }

        public async Task<DoctorAvailabilityResponse> GetById(int id)
        {
            DoctorAvailabilityResponse response = new();
            try
            {
                var result = await _doctorAvailabilityRepository.GetEntityBy(id);
                List<DoctorAvailabilityGetAllDto> doctorAvailability = ((List<DoctorAvailability>)result.Data)
                                                        .Select(doctorAvailability => new DoctorAvailabilityGetAllDto()
                                                        {
                                                            AvailabilityId = doctorAvailability.AvailabilityId,
                                                            DoctorId = doctorAvailability.DoctorID,
                                                            AvailableDate = doctorAvailability.AvailableDate,
                                                            StarTime = doctorAvailability.StartTime,
                                                            EndTime = doctorAvailability.EndTime,
                                                        }).OrderByDescending(doctorAvailability => doctorAvailability.AvailableDate)
                                                        .ToList();
                response.IsSuccess = result.Success;
                response.Model = doctorAvailability;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "The specified doctor's availability registry couldn't be obtained.";
                _logger.LogError(response.Message, ex.ToString());
            }
            return response;
        }

        public async Task<DoctorAvailabilityResponse> SaveAsync(DoctorAvailabilitySaveDto dto)
        {
            DoctorAvailabilityResponse response = new();
            try
            {
                DoctorAvailability doctorAvailability = new()
                {
                    AvailabilityId = dto.AvailabilityId,
                    DoctorID = dto.DoctorId,
                    AvailableDate = dto.AvailableDate,
                    StartTime = dto.StarTime,
                    EndTime = dto.EndTime,
                };
                var result = await _doctorAvailabilityRepository.Save(doctorAvailability);

                response.IsSuccess = result.Success;
                response.Model = doctorAvailability;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "The doctor's availability couldn't be saved.";
                _logger.LogError(response.Message, ex.ToString());
            }
            return response;
        }

        public async Task<DoctorAvailabilityResponse> UpdateAsync(int id, DoctorAvailabilityUpdateDto dto)
        {
            DoctorAvailabilityResponse response = new();
            try
            {
                DoctorAvailability doctorAvailability = new()
                {
                    AvailabilityId = dto.AvailabilityId,
                    DoctorID = dto.DoctorId,
                    AvailableDate = dto.AvailableDate,
                    StartTime = dto.StarTime,
                    EndTime = dto.EndTime,
                };
                var result = await _doctorAvailabilityRepository.Update(id, doctorAvailability);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "The doctor's availability couldn't be updated.";
                _logger.LogError(response.Message, ex.ToString());
            }
            return response;
        }
    }
}
