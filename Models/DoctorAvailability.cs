namespace HospitalAppointmentSystem.Models
{
    public class DoctorAvailability
    {
        public int Id{ get; set; } // Primary key
        public DayOfWeek DayOfWeek { get; set; }
        // Part of the Day: Morning, Afternoon, Night
        //public string PartOfDay { get; set; }
        public DateTime StartTime { get; set; } // Start time of the availability slot
        public DateTime EndTime { get; set; } // End time of the availability slot

        // Foreign key
        public int DoctorId { get; set; } // Foreign key to Doctor

        // Navigation property
        public Doctor Doctor { get; set; } // Reference to the Doctor
    }
}
