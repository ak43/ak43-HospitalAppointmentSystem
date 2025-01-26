using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models
{
    public class DoctorPatient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
