using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Models
{
    public class Doctor : User
    {
        public string Specialization { get; set; }
        public string Availability { get; set; }


        // Navigation Properties
        public Department Department { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        
    }
}
