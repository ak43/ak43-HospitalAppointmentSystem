using HospitalAppointmentSystem.Models;
using System.Threading.Channels;

namespace HospitalAppointmentSystem.Models
{
    public class Patient : User 
    {
        public string MRN { get; set; }
        
        public int Age { get; set; }
        public string MedicalHistory { get; set; }
        // Emergency contact info (contact name, phoneNumber, email)
        

        // Navigation property
        public Department Department { get; set; }
        public Doctor Doctor { get; set; }
    }
}
