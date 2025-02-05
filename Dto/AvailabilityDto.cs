﻿namespace HospitalAppointmentSystem.Dto
{
    public class AvailabilityDto
    {
        public int Id { get; set; } 
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; } 
        public int DoctorId { get; set; } 
    }
}
