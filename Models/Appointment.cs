using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;
using HospitalAppointmentSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Models
{
    [PrimaryKey("Id")]
    public class Appointment
    {
        public int Id { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public string? Status { get; set;}

        // Foreign keys
        public int DoctorId { get; set; } // Foreign key to Doctor
        public int PatientId { get; set; } // Foreign key to Patient

        // Navigation properties
        public Doctor Doctor { get; set; } // Reference to the Doctor
        public Patient Patient { get; set; }// Reference to the Patient
    }
}
