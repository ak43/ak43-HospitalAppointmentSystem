using HospitalAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

namespace HospitalAppointmentSystem.Models
{
    public class Patient : Person
    {
        public string MRN { get; set; }
        public int Age { get; set; }
        public string? MedicalHistory { get; set; }
        public int DoctorId{ get; set; }

        //public int DoctorId { get; set; }
        // Emergency contact info (contact name, phoneNumber, email)

        // Navigation property for appointments
        public Doctor Doctor { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
