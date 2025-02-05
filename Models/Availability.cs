namespace HospitalAppointmentSystem.Models
{
    public class Availability
    {
        public Availability()
        {
            
        }
        public int Id{ get; set; } // Primary key
        public DayOfWeek DayOfWeek { get; set; }
        // Part of the Day: Morning, Afternoon, Night
        //public string PartOfDay { get; set; }
        public TimeSpan StartTime { get; set; } // Start time of the availability slot
        public TimeSpan EndTime { get; set; } // End time of the availability slot
        //var elevenAM = new TimeOnly(11, 0)
        
        // Foreign key
        public int DoctorId { get; set; } // Foreign key to Doctor

        // Navigation property
        public Doctor Doctor { get; set; } // Reference to the Doctor
    }
}
