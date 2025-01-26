namespace HospitalAppointmentSystem.Models
{
    public class DoctorAvailability
    {
        public int iD { get; set; } // Primary key
        public DateTime StartTime { get; set; } // Start time of the availability slot
        public DateTime EndTime { get; set; } // End time of the availability slot

        // Foreign key
        public int DoctorId { get; set; } // Foreign key to Doctor

        // Navigation property
        public Doctor Doctor { get; set; } // Reference to the Doctor
    }
}
