using HospitalAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace HospitalAppointmentSystem.Models
{
    [PrimaryKey("Id")]
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public int DoctorId { get; set; }

        // Navigation properties
        //public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }

    }
}
