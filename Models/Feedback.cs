using HospitalAppointmentSystem.Models;
using System.Net.NetworkInformation;

namespace HospitalAppointmentSystem.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }

        // Navigation properties
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }

    }
}
