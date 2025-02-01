using HospitalAppointmentSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models
{
    public class Doctor : Person
    {
        public int Id { get; set; }
        public string Specialization { get; set; }
        public string Availability { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }


        // Navigation property for appointments
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        // Navigation property for availability slots
        public ICollection<DoctorAvailability> Availabilities { get; set; } = new List<DoctorAvailability>();

    }
}
