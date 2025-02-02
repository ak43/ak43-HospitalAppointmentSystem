namespace HospitalAppointmentSystem.Dto
{
    public class DoctorAvailabilityDto
    {
        public int Id { get; set; } 
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; } 
        public int DoctorId { get; set; } 
    }
}
