using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointments.Persistance.Context
{
    public partial class MedicalAppointmentContext : DbContext
    {
        public MedicalAppointmentContext(DbContextOptions<MedicalAppointmentContext> options) : base(options)
        {

        }

        #region "Appointment Entities"
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
        #endregion

        #region "Insurance Entities"
        public DbSet<InsuranceProviders> InsuranceProviders { get; set; }
        public DbSet<NetworkType> NetworkType { get; set; }
        #endregion

        #region "Medical Entities"
        public DbSet<AvailabilityModes> AvailabilityModes { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
        public DbSet<Specialities> Specialities { get; set; }
        #endregion

        #region "System Entities"
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Status> Status { get; set; }
        #endregion

        #region "Users Entities"
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Users> Users { get; set; }
        #endregion
    }
}
