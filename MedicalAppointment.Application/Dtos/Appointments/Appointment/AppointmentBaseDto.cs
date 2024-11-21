namespace MedicalAppointment.Application.Dtos.Appointments.Appointment
{
    public class AppointmentBaseDto
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
