using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateOnly AppointmentDate { get; set; } 
        public bool IsConfirmed { get; set; } 
        public string Status { get; set;}

        // Navigation properties
        public Department Department { get; set;  }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }  
    }
}
